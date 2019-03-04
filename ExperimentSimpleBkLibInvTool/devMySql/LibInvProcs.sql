/*
 * Data inserts, deletions and updates.
 */

-- -----------------------------------------------------
-- procedure UpdateAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`UpdateAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `UpdateAuthor`(
    IN LastName VARCHAR(20),
    IN FirstName VARCHAR(20),
    IN MiddleName VARCHAR(20),
    IN DOB VARCHAR(4),
    IN DOD VARCHAR(4)
)
DETERMINISTIC
BEGIN

    UPDATE authorstab 
        SET 
            authorstab.MiddleName = MiddleName,
            authorstab.YearOfBirth = DOB,
            authorstab.YearOfDeath = DOD
        WHERE authorstab.LastName = LastName AND authorstab.FirstName = FirstName;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addAuthor`(
    IN authorLastName VARCHAR(20),
    IN authorFirstName VARCHAR(20),
    IN authorMiddleName VARCHAR(20),
    IN dob VARCHAR(4),
    IN dod VARCHAR(4),
    OUT primaryKey INT
)
DETERMINISTIC
BEGIN

    INSERT INTO authorstab (authorstab.LastName, authorstab.FirstName, authorstab.MiddleName, authorstab.YearOfBirth, authorstab.YearOfDeath)
        VALUES(authorLastName, authorFirstName, authorMiddleName, dob, dod);
    SET primaryKey := LAST_INSERT_ID();

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addOrUpdateBookDataInTables
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addOrUpdateBookDataInTables`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addOrUpdateBookDataInTables`
(
    IN categoryKey INT,
    IN authorKey INT,
    IN titleKey INT,
    IN formatKey INT,
    IN copyright VARCHAR(4), IN iSBNumber VARCHAR(32), IN edition INT, IN printing INT, IN publisher VARCHAR(45), IN outOfPrint TINYINT,
    IN seriesKey INT, IN volumeNumber INT,
    IN bkStatusKey INT, IN bkConditionKey INT, physicalDescStr VARCHAR(256), IN iSignedByAuthor TINYINT, IN haveRead TINYINT,
    IN isOwned TINYINT, IN isWishListed TINYINT,
    IN isForSale TINYINT, IN askingPrice DOUBLE, IN estimatedValue DOUBLE,
    IN bookDescription VARCHAR(1024),
    IN myRating DOUBLE, IN amazonRating DOUBLE, IN goodReadsRating DOUBLE,
    OUT bookKey INT
)
DETERMINISTIC
BEGIN

    -- All book data that can be converted to keys have been converted, insert or update tables as necessary.
    -- All book data except for purchasing data will be added directly or indirectly from this procedure.
    -- Purchasing data will be added in the buyBook Procedure, that calls this procedure.
    -- Each independent portion of the data will have it's own add procedure that will be called here.

    -- If the author isn't found then the user has to add the author before they add any books or Series by the author.
    if @authorKey > 0 AND @formatKey > 0 THEN
        SET bookKey = findBookKeyFromKeys(authorKey, titleKey, formatKey);
        IF bookKey < 1 THEN
            -- Handle the case where the book is wishlisted.
            -- The formats will be different.
            SET bookKey = findBookKeyFromKeys(authorKey, titleKey, findFormatKeyFromStr('Not In Library'));
        END IF;
        IF bookKey < 1 THEN
            INSERT INTO bookinfo (bookinfo.AuthorFKbi, bookinfo.TitleFKbi, bookinfo.CategoryFKbi, bookinfo.BookFormatFKbi, bookinfo.SeriesFKbi)
                VALUES (authorKey, titleKey, categoryKey, formatKey, seriesKey);
            SET bookKey := LAST_INSERT_ID();
        ELSE
            UPDATE bookinfo
            SET
                bookinfo.AuthorFKbi = authorKey,
                bookinfo.TitleFKbi = titleKey,
                bookinfo.CategoryFKbi = categoryKey,
                bookinfo.BookFormatFKbi = formatKey,
                bookinfo.SeriesFKbi = seriesKey
            WHERE bookinfo.idBookInfo = bookKey;
        END IF;

        CALL insertOrUpdatePublishing(bookKey, copyright, iSBNumber, edition, printing, publisher, outOfPrint);
        CALL insertOrUpdateOwned(bookKey, isOwned, isWishListed);
        CALL insertOrUpdateBookCondition(bookKey, bkStatusKey, bkConditionKey, physicalDescStr, iSignedByAuthor, haveRead);
        IF seriesKey > 0 THEN
            CALL insertOrUpdateVolumeInSeries(bookKey, volumeNumber, seriesKey);
        END IF;
        IF isOwned > 0 THEN
            CALL insertOrUpdateForSale(bookKey, isForSale, askingPrice, estimatedValue);
        END IF;
        IF myRating IS NOT NULL THEN
            CALL insertOrUpdateBookRatings(bookKey, myRating, amazonRating, goodReadsRating);
        END IF;
        IF bookDescription IS NOT NULL OR LENGTH(bookDescription) > 0 THEN
            -- Try to save space if there is no description.
            CALL insertOrUpdateSynopsis(bookKey, bookDescription);
        END IF;
            
    END IF;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addBookToLibrary
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addBookToLibrary`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addBookToLibrary`
(
    IN categoryName VARCHAR(45),
    IN authorLastName VARCHAR(20), IN authorFirstName VARCHAR(20),
    IN titleStr VARCHAR(128), 
    IN bookFormatStr VARCHAR(45),
    IN copyright VARCHAR(4), IN iSBNumber VARCHAR(32), IN edition INT, IN printing INT, IN publisher VARCHAR(45), IN outOfPrint TINYINT,
    IN seriesName VARCHAR(128), IN volumeNumber INT,
    IN bkStatus VARCHAR(45), IN bkCondition VARCHAR(16), physicalDescStr VARCHAR(256), IN iSignedByAuthor TINYINT, IN haveRead TINYINT,
    IN isOwned TINYINT, IN isWishListed TINYINT,
    IN isForSale TINYINT, IN askingPrice DOUBLE, IN estimatedValue DOUBLE,
    IN bookDescription VARCHAR(1024),
    IN myRating DOUBLE, IN amazonRating DOUBLE, IN goodReadsRating DOUBLE,
    OUT bookKey INT
)
DETERMINISTIC
BEGIN

    -- Convert all strings that are keys in other tables to the key value and add or update the book info.

    SET @titleKey = 0, @formatKey = 0, @authorKey = 0, @seriesKey = 0;
    SET @authorKey = findAuthorKey(authorFirstName, authorLastName);
    SET @formatKey = findFormatKeyFromStr(BookFormatStr);
    
    -- If the author isn't found then the user has to add the author before they add any books or Series by the author.
    IF @authorKey > 0 AND @formatKey > 0 THEN
        SET @seriesKey = findSeriesKeyByAuthKeyTitle(@authorKey, SeriesName);
        SET @titleKey = insertTitleIfNotExist(titleStr);
        SET @categoryKey = findCategoryKeyFromStr(categoryName);
        Set @conditionKey = findbkConditionKey(bkCondition);
        Set @statusKey = findbkStatusKey(bkStatus);

        CALL addOrUpdateBookDataInTables(@categoryKey, @authorKey, @titleKey, @formatKey, copyright, iSBNumber, edition, printing, publisher, outOfPrint,
            @seriesKey, volumeNumber, @statusKey, @conditionKey, physicalDescStr, iSignedByAuthor, haveRead, isOwned, isWishListed,
            isForSale, askingPrice, estimatedValue, bookDescription, myRating, amazonRating, goodReadsRating, bookKey
        );
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure buyBook
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`buyBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `buyBook`
(
    IN categoryName VARCHAR(45),
    IN authorLastName VARCHAR(20), IN authorFirstName VARCHAR(20),
    IN titleStr VARCHAR(128), 
    IN bookFormatStr VARCHAR(45),
    IN copyright VARCHAR(4), IN iSBNumber VARCHAR(32), IN edition INT, IN printing INT, IN publisher VARCHAR(45), IN outOfPrint TINYINT,
    IN seriesName VARCHAR(128), IN volumeNumber INT,
    IN physicalDescStr VARCHAR(256), IN iSignedByAuthor TINYINT,
    IN bookDescription VARCHAR(1024),
    IN purchaseDate DATE, IN listPrice DOUBLE, IN pricePaid DOUBLE, IN vendor VARCHAR(64),
    OUT bookKey INT    -- allows the calling program or procedure to test for failure.
)
DETERMINISTIC
BEGIN

    -- Assumptions when the book has just been bought.
    SET @estimatedValue = listPrice - 1.00;
    SET @haveRead = 0, @IsOwned = 1, @IsForSale = 0, @IsWishListed = 0;
    SET @bkStatus = 'New', @bkCondition = 'Excellent';
    SET @newBookRatings = NULL;

    CALL addBookToLibrary(categoryName, authorLastName, authorFirstName, titleStr, bookFormatStr, copyright, iSBNumber, edition, printing, publisher, outOfPrint,
        seriesName, volumeNumber, @bkStatus, @bkCondition, physicalDescStr, iSignedByAuthor, @haveRead, @IsOwned, @IsWishListed, @IsForSale, @estimatedValue, @estimatedValue, 
        bookDescription, @newBookRatings, @newBookRatings, @newBookRatings, bookKey
    );
    IF bookKey IS NOT NULL AND bookKey > 0 THEN
        CALL insertOrUpdatePurchaseInfo(bookKey, purchaseDate, listPrice, pricePaid, vendor);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure deleteAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`deleteAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `deleteAuthor`
(
    IN authorLast VARCHAR(20),
    IN authorFirst VARCHAR(20),
    IN authorMiddle VARCHAR(20)
)
DETERMINISTIC
BEGIN
    -- This procedure deletes everything associated with the specified author
    -- including books, series and volumes in series. It affects almost every table
    -- in this database.
    -- Do not delete formats and categories.

    DELETE a, BKI, s, v, bc, pub, pur, o, fs, BDesk
        FROM authorstab AS a 
        LEFT JOIN series AS s ON s.AuthorOfSeries = a.idAuthors
        LEFT JOIN volumeinseries AS v ON v.SeriesFK = s.idSeries
        INNER JOIN bookinfo AS BKI ON BKI.AuthorFKbi = a.idAuthors
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        WHERE a.LastName = authorLast AND a.FirstName = authorFirst AND a.MiddleName = authorMiddle;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure deleteBook
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`deleteBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `deleteBook`
(
    IN authorLast VARCHAR(20),
    IN authorFirst VARCHAR(20),
    IN titleStr VARCHAR(128),
    IN formatStr VARCHAR(45)
)
DETERMINISTIC
BEGIN
    -- Do not delete authors, titles, series, formats or categories. These may be shared with other books.

    DELETE BKI, bc, pub, pur, v, o, fs, BDesk
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        WHERE a.LastName = authorLast AND a.FirstName = authorFirst AND t.TitleStr = titleStr and bf.FormatName = formatStr;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdatePublishing
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdatePublishing`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdatePublishing` 
(
    IN bookKey INT,
    IN copyright VARCHAR(4),
    IN iSBNumber VARCHAR(30),
    IN edition INT,
    IN printing INT,
    IN publisher VARCHAR(45),
    IN outOfPrint TINYINT
)
DETERMINISTIC
BEGIN

   --  DECLARE testCopyright VARCHAR(4);
    
    SET @testKey = NULL;
    SELECT publishinginfo.Copyright INTO @testCopyright FROM publishinginfo WHERE publishinginfo.BookFKPubI = bookKey;
    
    IF @testCopyright IS NULL THEN
        INSERT INTO publishinginfo (publishinginfo.BookFKPubI, publishinginfo.Copyright, publishinginfo.ISBNumber, publishinginfo.Edition, publishinginfo.Printing, publishinginfo.Publisher, publishinginfo.OutOfPrint)
            VALUES(bookKey, copyright, iSBNumber, edition, printing, publisher, outOfPrint)
        ;
    ELSE
        UPDATE publishinginfo
            SET
                publishinginfo.Copyright = copyright,
                publishinginfo.ISBNumber = iSBNumber,
                publishinginfo.Edition = edition,
                publishinginfo.Printing = printing,
                publishinginfo.Publisher = publisher,
                publishinginfo.OutOfPrint = outOfPrint
            WHERE publishinginfo.BookFKPubI = bookKey;
    END IF;
END$$

DELIMITER ;getAllAuthorsData

-- -----------------------------------------------------
-- procedure insertOrUpdateOwned
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateOwned`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateOwned` 
(
    IN bookKey INT,
    IN isOwned TINYINT,
    IN isWishListed TINYINT
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT owned.BookFKo INTO @testKey FROM owned WHERE owned.BookFKo = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO owned (owned.BookFKo, owned.IsOwned, owned.IsWishListed)
            VALUES(bookKey, isOwned, isWishListed)
        ;
    ELSE
        UPDATE Owned
            SET
                owned.isOwned = isOwned,
                owned.IsWishListed = isWishListed
            WHERE owned.BookFKo = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdateBookCondition
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateBookCondition`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateBookCondition`
(
    IN bookKey INT,
    IN statusKey INT,
    IN conditionKey INT,
    IN physicalDescStr VARCHAR(256),
    IN signedByAuthor TINYINT,
    IN haveRead TINYINT
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT bookcondition.BookFKCond INTO @testKey FROM bookcondition WHERE bookcondition.BookFKCond = bookKey;

    IF @testKey IS NULL THEN
        INSERT INTO bookcondition (bookcondition.BookFKCond, bookcondition.NewOrUsed, bookcondition.ConditionOfBook, bookcondition.PhysicalDescriptionStr, bookcondition.IsSignedByAuthor, bookcondition.BookHasBeenRead)
            VALUES(bookKey, statusKey, conditionKey, physicalDescStr, signedByAuthor, haveRead)
        ;
    ELSE
        UPDATE bookcondition
            SET
                bookcondition.NewOrUsed = statusKey,
                bookcondition.ConditionOfBook = conditionKey,
                bookcondition.PhysicalDescriptionStr = physicalDescStr,
                bookcondition.IsSignedByAuthor = signedByAuthor,
                bookcondition.BookHasBeenRead = haveRead
            WHERE bookcondition.BookFKCond = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdateVolumeInSeries
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateVolumeInSeries`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateVolumeInSeries` 
(
    IN bookKey INT,
    IN volumeNumber INT,
    IN seriesKey INT
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT volumeinseries.BookFKvs INTO @testKey FROM volumeinseries WHERE volumeinseries.BookFKvs = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO volumeinseries (volumeinseries.BookFKvs, volumeinseries.SeriesFK, volumeinseries.VolumeNumber)
            VALUES(bookKey, seriesKey, volumeNumber)
        ;
    ELSE
        UPDATE volumeinseries
            SET
                volumeinseries.SeriesFK = seriesKey,
                volumeinseries.VolumeNumber = volumeNumber
            WHERE VolumeInSeries.BookFKvs = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdateForSale()
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateForSale`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateForSale`
(
    IN bookKey INT,
    IN isForSale TINYINT,
    IN askingPrice DOUBLE,
    IN estimatedValue DOUBLE
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT forsale.BookFKfs INTO @testKey FROM forsale WHERE forsale.BookFKfs = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO forsale (forsale.BookFKfs, forsale.IsForSale, forsale.AskingPrice, forsale.EstimatedValue)
            VALUES(bookKey, isForSale, askingPrice, estimatedValue)
        ;
    ELSE
        UPDATE forsale
            SET
                forsale.isForSale = isForSale,
                forsale.AskingPrice = askingPrice,
                forsale.EstimatedValue = estimatedValue
            WHERE forsale.BookFKfs = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdateSynopsis
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateSynopsis`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateSynopsis`
(
    IN bookKey INT,
    IN bookDescription VARCHAR(1024)
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT bksynopsis.BookFKsyop INTO @testKey FROM bksynopsis WHERE bksynopsis.BookFKsyop = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO bksynopsis (bksynopsis.BookFKsyop, bksynopsis.StoryLine)
            VALUES(bookKey, bookDescription)
        ;
    ELSE
        UPDATE bksynopsis
            SET
                bksynopsis.StoryLine = bookDescription
            WHERE bksynopsis.BookFKsyop = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdatePurchaseInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdatePurchaseInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdatePurchaseInfo`
(
    IN bookKey INT,
    IN purchaseDate DATE,
    IN listPrice DOUBLE,
    IN pricePaid DOUBLE,
    vendor VARCHAR(64)
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT purchaseinfo.BookFKPurI INTO @testKey FROM purchaseinfo WHERE purchaseinfo.BookFKPurI = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO purchaseinfo
            (purchaseinfo.BookFKPurI, purchaseinfo.PurchaseDate, purchaseinfo.ListPrice, purchaseinfo.PaidPrice, purchaseinfo.Vendor)
            VALUES(bookKey, purchaseDate, listPrice, pricePaid, vendor)
        ;
    ELSE
        UPDATE purchaseinfo
            SET
                purchaseinfo.PurchaseDate = purchaseDate,
                purchaseinfo.ListPrice = listPrice,
                purchaseinfo.PaidPrice = pricePaid,
                purchaseinfo.Vendor = vendor
            WHERE purchaseinfo.BookFKPurI = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure insertOrUpdateBookRatings
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`insertOrUpdateBookRatings`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `insertOrUpdateBookRatings`
(
    IN bookKey INT,
    IN myRatings DOUBLE,
    IN amazonRatings DOUBLE,
    IN goodReadsRatings DOUBLE
)
DETERMINISTIC
BEGIN

    SET @testKey = NULL;
    SELECT ratings.BookFKRats INTO @testKey FROM ratings WHERE ratings.BookFKRats = bookKey;
    
    IF @testKey IS NULL THEN
        INSERT INTO ratings
            (ratings.BookFKRats, ratings.MyRatings, ratings.AmazonRatings, ratings.GoodReadsRatings)
            VALUES(bookKey, myRatings, amazonRatings, goodReadsRatings)
        ;
    ELSE
        UPDATE ratings
            SET
                ratings.MyRatings = myRatings,
                ratings.AmazonRatings = amazonRatings,
                ratings.GoodReadsRatings = goodReadsRatings
            WHERE ratings.BookFKRats = bookKey;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addCategory
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addCategory`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addCategory`
(
    IN categoryName VARCHAR(45),
    OUT primaryKey INT
)
DETERMINISTIC
BEGIN

    SET @categoryKey = NULL;

    SELECT bookcategories.idBookCategories INTO @categoryKey
        FROM bookcategories
        WHERE bookcategories.CategoryName = categoryName;

    -- Prevent adding the same category again to avoid breaking the unique key structure.

    IF @categoryKey IS NULL THEN
        INSERT INTO bookcategories (bookcategories.CategoryName) VALUES(categoryName);
        SET primaryKey := LAST_INSERT_ID();
    ELSE
        SET primaryKey := @categoryKey;
    END IF;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addFormat
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addFormat`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addFormat` (IN bookFormatStr VARCHAR(45), OUT primaryKey INT)
DETERMINISTIC
BEGIN

    SET @formatKey = findFormatKeyFromStr(bookFormatStr);
    
    -- Prevent adding the same format again to avoid breaking the unique key structure.
    IF @formatKey < 1 THEN
        INSERT INTO bookformat (bookformat.FormatName) VALUES(bookFormatStr);
        SET primaryKey := LAST_INSERT_ID();
    END IF;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addAuthorSeries
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addAuthorSeries`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addAuthorSeries`
(
    IN authorFirst VARCHAR(20),
    IN authorLast VARCHAR(20),
    IN seriesTitle VARCHAR(128)
)
DETERMINISTIC
BEGIN

    SET @authorKey = findAuthorKey(authorFirst, authorLast);

    IF @authorKey > 0 THEN
        SET @seriesKey = findSeriesKeyByAuthKeyTitle(@authorKey, seriesTitle);
    
        IF @seriesKey < 1 THEN
            INSERT INTO series (series.AuthorOfSeries, series.SeriesName) VALUES(@authorKey, seriesTitle);
        END IF;
    END IF;

    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addAuthorSeriesWithAuthorKey
-- Assumes author id was found externally from the database.
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addAuthorSeriesWithAuthorKey`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addAuthorSeriesWithAuthorKey`
(
    IN authorKey INT,
    IN seriesTitle VARCHAR(128)
)
DETERMINISTIC
BEGIN

    IF authorKey > 0 THEN
        SET @seriesKey = findSeriesKeyByAuthKeyTitle(authorKey, seriesTitle);
    
        IF @seriesKey < 1 THEN
            INSERT INTO series (series.AuthorOfSeries, series.SeriesName) VALUES(authorKey, seriesTitle);
        END IF;
    END IF;

    
END$$

DELIMITER ;


/*
 * Data retrieval functions and queries.
 */

-- -----------------------------------------------------
-- procedure getAllBooksInCategory
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksInCategory`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksInCategory`(IN categoryKey INT)
DETERMINISTIC
BEGIN

    SELECT
            BKI.idBookInfo, a.*, t.*, bf.*, BCat.*,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.*, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE BKI.CategoryFKbI = categoryKey
        ORDER BY a.LastName ASC, a.FirstName ASC, s.SeriesName ASC, v.VolumeNumber ASC, t.TitleStr ASC;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksInCategorySortedByMyRatings
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksInCategorySortedByMyRatings`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksInCategorySortedByMyRatings`(IN categoryKey INT)
DETERMINISTIC
BEGIN

    SELECT
            BKI.idBookInfo, a.*, t.*, bf.*, BCat.*,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.*, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = categoryKey
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE BKI.CategoryFKbI = categoryKey
        ORDER BY rts.MyRatings DESC, a.LastName ASC, a.FirstName ASC, s.SeriesName ASC, v.VolumeNumber ASC, t.TitleStr ASC;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBookDataSortedByMyRatings
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBookDataSortedByMyRatings`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBookDataSortedByMyRatings`()
DETERMINISTIC
BEGIN

    SELECT
            BKI.idBookInfo, a.*, t.*, bf.*, BCat.*,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.*, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        ORDER BY rts.MyRatings DESC, a.LastName ASC, a.FirstName ASC, s.SeriesName ASC, v.VolumeNumber ASC, t.TitleStr ASC;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getRatedBooksSortedByMyRating
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getRatedBooksSortedByMyRating`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getRatedBooksSortedByMyRating`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName, t.TitleStr, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright,
            s.SeriesName, v.VolumeNumber,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE rts.BookFKRats IS NOT NULL
        ORDER BY rts.MyRatings DESC; -- 5 stars being best and 1 star being worst.
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getRatedBooksSortedByAmazonRating
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getRatedBooksSortedByAmazonRating`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getRatedBooksSortedByAmazonRating`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName, t.TitleStr, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright,
            s.SeriesName, v.VolumeNumber,
            rts.AmazonRatings, rts.MyRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE rts.BookFKRats IS NOT NULL
        ORDER BY rts.AmazonRatings DESC; -- 5 stars being best and 1 star being worst.
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getRatedBooksSortedByGoodReadsRating
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getRatedBooksSortedByGoodReadsRating`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getRatedBooksSortedByGoodReadsRating`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName, t.TitleStr, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright,
            s.SeriesName, v.VolumeNumber,
            rts.GoodReadsRatings, rts.MyRatings, rts.AmazonRatings, 
            BDesk.StoryLine
        FROM bookinfo AS BKI
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE rts.BookFKRats IS NOT NULL
        ORDER BY rts.GoodReadsRatings DESC; -- 5 stars being best and 1 star being worst.
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksForSale
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksForSale`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksForSale`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            bf.FormatName, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            bc.IsSignedByAuthor, bCond.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr,
            fs.AskingPrice, fs.EstimatedValue,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCond ON bc.ConditionOfBook = bCond.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        WHERE o.IsOwned = 1 AND fs.IsForSale = 1 AND bc.BookHasBeenRead = 1
        ORDER BY a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksSignedByAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksSignedByAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksSignedByAuthor`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            bf.FormatName, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        WHERE bc.IsSignedByAuthor = 1
        ORDER BY BCat.CategoryName, a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksWithKeys
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksWithKeys`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksWithKeys`()
DETERMINISTIC
BEGIN

    SELECT
            BKI.idBookInfo, a.*, t.*, bf.*, BCat.*,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.*, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        ORDER BY BCat.CategoryName, a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooks
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooks`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooks`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            bf.FormatName, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings,
            BDesk.StoryLine
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        ORDER BY BCat.CategoryName, a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksNoKeysInLib
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksNoKeysInLib`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksNoKeysInLib`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            bf.FormatName, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            BDesk.StoryLine,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE o.IsOwned = 1
        ORDER BY a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksWithKeysInLib
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksWithKeysInLib`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksWithKeysInLib`()
DETERMINISTIC
BEGIN

    SELECT
            BKI.idBookInfo, a.*, t.*, bf.*, BCat.*,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.*, v.VolumeNumber,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            BDesk.StoryLine,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE o.IsOwned = 1
        ORDER BY a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllSeriesByThisAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllSeriesByThisAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllSeriesByThisAuthor`(
    IN AuthorLastName VARCHAR(20),
    IN AuthorFirstName VARCHAR(20)
)
DETERMINISTIC
BEGIN

    SELECT series.SeriesName, series.idSeries FROM series WHERE series.AuthorOfSeries = findauthorKey(AuthorFirstName, AuthorLastName);

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllSeriesData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllSeriesData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllSeriesData`()
DETERMINISTIC
BEGIN

    SELECT a.LastName, a.FirstName, s.SeriesName
        FROM series AS s
        INNER JOIN authorstab AS a
        ON a.idAuthors = s.AuthorOfSeries
        ORDER BY a.LastName, a.FirstName, s.SeriesName;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getBookData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getBookData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getBookData`(
    IN authorLast VARCHAR(20),
    IN authorFirst VARCHAR(20),
    IN titleStr VARCHAR(128),
    IN formatStr VARCHAR(45)
)
DETERMINISTIC
BEGIN

    -- No order by clause because it's returning only a single book.
    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            bf.FormatName, BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            bc.IsSignedByAuthor, bCondStr.ConditionOfBookStr, bstat.BkStatusStr, bc.PhysicalDescriptionStr, bc.BookHasBeenRead,
            pur.PurchaseDate, pur.ListPrice, pur.PaidPrice, pur.Vendor,
            o.IsOwned, o.IsWishListed,
            fs.IsForSale, fs.AskingPrice, fs.EstimatedValue,
            BDesk.StoryLine,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN bkconditions AS bCondStr ON bc.ConditionOfBook = bCondStr.idBkConditions
        LEFT JOIN bkstatuses AS bstat ON bc.NewOrUsed = bstat.idBkStatus
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN purchaseinfo AS pur ON pur.BookFKPurI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN forsale AS fs ON fs.BookFKfs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE a.LastName = authorLast AND a.FirstName = authorFirst AND t.TitleStr = titleStr and bf.FormatName = formatStr;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllWishListBooks
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllWishListBooks`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllWishListBooks`()
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            BCat.CategoryName,
            pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            s.SeriesName, v.VolumeNumber,
            bc.BookHasBeenRead,
            o.IsOwned, o.IsWishListed
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN owned AS o ON o.BookFKo = BKI.idBookInfo
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        WHERE o.IsWishListed = 1
        ORDER BY a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksByThisAuthor
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksByThisAuthor`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksByThisAuthor` 
(
    IN authorLastName VARCHAR(20),
    IN authorFirstName VARCHAR(20)
)
DETERMINISTIC
BEGIN

    SET @authorKey = findauthorKey(authorFirstName, authorLastName);

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            BCat.CategoryName,
            pub.ISBNumber, pub.Copyright, pub.Edition, pub.Publisher, pub.OutOfPrint, pub.Printing,
            bc.IsSignedByAuthor, bc.BookHasBeenRead,
            s.SeriesName, v.VolumeNumber,
            BDesk.StoryLine,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN publishinginfo AS pub ON pub.BookFKPubI = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE BKI.AuthorFKbi = @authorKey
        ORDER BY s.SeriesName, v.VolumeNumber, t.TitleStr;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBooksThatWereRead
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBooksThatWereRead`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBooksThatWereRead` 
(
)
DETERMINISTIC
BEGIN

    SELECT
            a.LastName, a.FirstName,
            t.TitleStr,
            BCat.CategoryName,
            s.SeriesName, v.VolumeNumber,
            BDesk.StoryLine,
            rts.MyRatings, rts.AmazonRatings, rts.GoodReadsRatings
        FROM bookinfo AS BKI 
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookcategories AS BCat ON BCat.idBookCategories = BKI.CategoryFKbI
        LEFT JOIN bookcondition AS bc ON bc.BookFKCond = BKI.idBookInfo
        LEFT JOIN series AS s ON s.idSeries = BKI.SeriesFKbi
        LEFT JOIN volumeinseries AS v ON v.BookFKvs = BKI.idBookInfo
        LEFT JOIN bksynopsis AS BDesk ON BDesk.BookFKsyop = BKI.idBookInfo 
        LEFT JOIN ratings AS rts ON rts.BookFKRats = BKI.idBookInfo 
        WHERE bc.BookHasBeenRead = 1
        ORDER BY a.LastName, a.FirstName, s.SeriesName, v.VolumeNumber, t.TitleStr;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBookCategoriesWithKeys
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBookCategoriesWithKeys`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBookCategoriesWithKeys` ()
DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the categories to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bookcategories.CategoryName, bookcategories.idBookCategories FROM bookcategories;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllBookFormatsWithKeys
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllBookFormatsWithKeys`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllBookFormatsWithKeys`()
DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the formats to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bookformat.FormatName, bookformat.idFormat FROM bookformat;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllStatusesWithKeys
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllStatusesWithKeys`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllStatusesWithKeys`()
DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the statuses to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bkstatuses.BkStatusStr, bkstatuses.idBkStatus FROM bkstatuses;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllConditionsWithKeys
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllConditionsWithKeys`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllConditionsWithKeys`()
DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the conditions to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bkconditions.ConditionOfBookStr, bkconditions.idBkConditions FROM bkconditions;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAllAuthorsData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAllAuthorsData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAllAuthorsData` ()
DETERMINISTIC
BEGIN

/*
 * Could be used to create a select control of authors for a book or possibly a typing completion text control.
 */

    SELECT * FROM authorstab
        ORDER BY authorstab.LastName, authorstab.FirstName, authorstab.MiddleName;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getAuthorDataByLastName
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getAuthorDataByLastName`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getAuthorDataByLastName`
(
    IN authorLastName VARCHAR(20)
)
DETERMINISTIC
BEGIN
    
    /*
     * Will return multiple authors with the same last name, e.g. if the last name is Herbert
     * it will return both Frank Herbert and Brian Herbert.
     */
    SELECT * FROM authorstab WHERE authorstab.LastName = authorLastName;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getThisAuthorsData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getThisAuthorsData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getThisAuthorsData`
(
    IN authorLastName VARCHAR(20),
    IN authorFirstName VARCHAR(20)
)
DETERMINISTIC
BEGIN

    SELECT * FROM authorstab WHERE authorstab.LastName = authorLastName AND authorstab.FirstName = authorFirstName;

END$$

DELIMITER ;

/*
 * Start of procedures that allow the user to update books in a limited manner.
 */

-- -----------------------------------------------------
-- procedure bookSold
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`bookSold`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `bookSold` 
(
    IN authorFirstName VARCHAR(20),
    IN authorLastName VARCHAR(20),
    IN bookTitle VARCHAR(128),
    IN bookFormat VARCHAR(45)
)
DETERMINISTIC
BEGIN

    SET @isOwned = 0, @isWishListed = 0;

    SET @bookKey = findBookKeyFast(authorLastName, authorFirstName, bookTitle, bookFormat);
    
    CALL insertOrUpdateOwned(@bookKey, @isOwned, @isWishListed);

    DELETE FROM forsale WHERE forsale.BookFKfs = @bookKey;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure finishedReadingBook
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`finishedReadingBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `finishedReadingBook` (
    IN authorFirstName VARCHAR(20),
    IN authorLastName VARCHAR(20),
    IN bookTitle VARCHAR(128),
    IN bookFormat VARCHAR(45)
)
DETERMINISTIC
BEGIN

    SET @haveReadBook = 1;

    SET @bookKey = findBookKeyFast(authorLastName, authorFirstName, bookTitle, bookFormat);

    UPDATE bookcondition
        SET
            bookcondition.BookHasBeenRead = @haveReadBook
        WHERE bookcondition.BookFKCond = @bookKey;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure putBookUpForSale
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`putBookUpForSale`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `putBookUpForSale`
(
    IN authorFirstName VARCHAR(20),
    IN authorLastName VARCHAR(20),
    IN bookTitle VARCHAR(128),
    IN bookFormat VARCHAR(45),
    IN askingPrice DOUBLE,
    IN estimatedValue DOUBLE
)
DETERMINISTIC
BEGIN

    SET @isForSale = 1;

    SET @bookKey = findBookKeyFast(authorLastName, authorFirstName, bookTitle, bookFormat);

    CALL insertOrUpdateForSale(@bookKey, @isForSale, askingPrice, estimatedValue);

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure rateThisBook
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`rateThisBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `rateThisBook`
(
    IN authorFirstName VARCHAR(20),
    IN authorLastName VARCHAR(20),
    IN bookTitle VARCHAR(128),
    IN bookFormat VARCHAR(45),
    IN myRating DOUBLE,
    IN amazonRating DOUBLE,
    IN goodReadsRating DOUBLE
)
DETERMINISTIC
BEGIN

    SET @bookKey = findBookKeyFast(authorLastName, authorFirstName, bookTitle, bookFormat);

    CALL insertOrUpdateBookRatings(@bookKey, myRating, amazonRating, goodReadsRating);

END$$

DELIMITER ;

/*
 * Once only code called during installation or testing.
 */

-- -----------------------------------------------------
-- procedure initBookInventoryTool
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`initBookInventoryTool`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `initBookInventoryTool` ()
DETERMINISTIC
BEGIN

    DECLARE primaryKey INT;

-- Initialize some basic formats, user can add more later.
    IF (SELECT COUNT(*) FROM bookformat) < 1 THEN
        CALL addFormat('Not In Library', primaryKey);
        CALL addFormat('Hardcover', primaryKey);
        CALL addFormat('Trade Paperback', primaryKey);
        CALL addFormat('Mass Market Paperback', primaryKey);
        CALL addFormat('eBook PDF', primaryKey);
        CALL addFormat('eBook Kindle', primaryKey);
        CALL addFormat('eBook iBooks', primaryKey);
        CALL addFormat('eBook EPUB', primaryKey);
        CALL addFormat('eBook HTML', primaryKey);
    END IF;

-- Initialize some basic categories, user can add more later.
    IF (SELECT COUNT(*) FROM bookcategories) < 1 THEN
        CALL addCategory('Non-Fiction', primaryKey);
        CALL addCategory('Non-Fiction: Biography', primaryKey);
        CALL addCategory('Non-Fiction: Biology', primaryKey);
        CALL addCategory('Non-Fiction: Computer', primaryKey);
        CALL addCategory('Non-Fiction: Electrical Engineering', primaryKey);
        CALL addCategory('Non-Fiction: History', primaryKey);
        CALL addCategory('Textbook', primaryKey);
        CALL addCategory('Poetry', primaryKey);
        CALL addCategory('Art', primaryKey);
        CALL addCategory('Dictionary', primaryKey);
        CALL addCategory('Encyclopedia', primaryKey);
        CALL addCategory('Fiction', primaryKey);
        CALL addCategory('Fiction: Anime', primaryKey);
        CALL addCategory('Fiction: Fantasy', primaryKey);
        CALL addCategory('Fiction: Horror', primaryKey);
        CALL addCategory('Fiction: Romance', primaryKey);
        CALL addCategory('Fiction: Science Fiction', primaryKey);
        CALL addCategory('Fiction: Western', primaryKey);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getPublishingInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getPublishingInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getPublishingInfo` (
    IN bookKey INT
)
DETERMINISTIC
BEGIN

    IF bookKey > 0 THEN
        SELECT * FROM publishinginfo WHERE BookFKPubI = bookKey;
    ELSE
        SELECT * FROM publishinginfo;
    END IF;
    
END$$

DELIMITER ;

--
-- New add and get procedures added 02/23/2019.
--

-- -----------------------------------------------------
-- procedure addPublishingInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addPublishingInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addPublishingInfo` (
    IN bookKey INT,
    IN copyright VARCHAR(4),
    IN iSBNumber VARCHAR(30),
    IN edition INT,
    IN printing INT,
    IN publisher VARCHAR(45),
    IN outOfPrint TINYINT
)
DETERMINISTIC
BEGIN

    IF bookKey > 0 THEN
        INSERT INTO publishinginfo (publishinginfo.BookFKPubI, publishinginfo.Copyright, publishinginfo.ISBNumber, publishinginfo.Edition, publishinginfo.Printing, publishinginfo.Publisher, publishinginfo.OutOfPrint)
            VALUES(bookKey, copyright, iSBNumber, edition, printing, publisher, outOfPrint);
    END IF;
    
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addOwnerShipData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addOwnerShipData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addOwnerShipData` 
(
    IN bookKey INT,
    IN isOwned TINYINT,
    IN isWishListed TINYINT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        INSERT INTO owned (owned.BookFKo, owned.IsOwned, owned.IsWishListed)
            VALUES(bookKey, isOwned, isWishListed);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getOwnerShipData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getOwnerShipData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getOwnerShipData` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM owned WHERE BookFKPubI = bookKey;
    ELSE
        SELECT * FROM owned;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addConditionToBook
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addConditionToBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addConditionToBook`
(
    IN bookKey INT,
    IN statusKey INT,
    IN conditionKey INT,
    IN physicalDescStr VARCHAR(256),
    IN signedByAuthor TINYINT,
    IN haveRead TINYINT
)
DETERMINISTIC
BEGIN

    IF bookKey > 0 THEN
        INSERT INTO bookcondition (bookcondition.BookFKCond, bookcondition.NewOrUsed, bookcondition.ConditionOfBook, bookcondition.PhysicalDescriptionStr, bookcondition.IsSignedByAuthor, bookcondition.BookHasBeenRead)
            VALUES(bookKey, statusKey, conditionKey, physicalDescStr, signedByAuthor, haveRead);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getBookCondition
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getBookCondition`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getBookCondition` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM bookcondition WHERE BookFKCond = bookKey;
    ELSE
        SELECT * FROM bookcondition;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addForSaleDataToBook()
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addForSaleDataToBook`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addForSaleDataToBook`
(
    IN bookKey INT,
    IN isForSale TINYINT,
    IN askingPrice DOUBLE,
    IN estimatedValue DOUBLE
)
DETERMINISTIC
BEGIN

    IF bookKey > 0 THEN
        INSERT INTO forsale (forsale.BookFKfs, forsale.IsForSale, forsale.AskingPrice, forsale.EstimatedValue)
            VALUES(bookKey, isForSale, askingPrice, estimatedValue);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getBookForSaleData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getBookForSaleData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getBookForSaleData` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM foresale WHERE BookFKfs = bookKey;
    ELSE
        SELECT * FROM foresale;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addPurchaseInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addPurchaseInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addPurchaseInfo`
(
    IN bookKey INT,
    IN purchaseDate DATE,
    IN listPrice DOUBLE,
    IN pricePaid DOUBLE,
    vendor VARCHAR(64)
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        INSERT INTO purchaseinfo
            (purchaseinfo.BookFKPurI, purchaseinfo.PurchaseDate, purchaseinfo.ListPrice, purchaseinfo.PaidPrice, purchaseinfo.Vendor)
            VALUES(bookKey, purchaseDate, listPrice, pricePaid, vendor);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getPurchaseInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getPurchaseInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getPurchaseInfo` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM purchaseinfo WHERE BookFKPurI = bookKey;
    ELSE
        SELECT * FROM purchaseinfo;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addBookRatings
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addBookRatings`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addBookRatings`
(
    IN bookKey INT,
    IN myRatings DOUBLE,
    IN amazonRatings DOUBLE,
    IN goodReadsRatings DOUBLE
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        INSERT INTO ratings
            (ratings.BookFKRats, ratings.MyRatings, ratings.AmazonRatings, ratings.GoodReadsRatings)
            VALUES(bookKey, myRatings, amazonRatings, goodReadsRatings);
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getRatings
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getRatings`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getRatings` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM ratings WHERE BookFKRats = bookKey;
    ELSE
        SELECT * FROM ratings;
    END IF;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getTableColumnData
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getTableColumnData`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getTableColumnData` 
(
    IN tableName varchar(128)
)
DETERMINISTIC
BEGIN
    
    SELECT COLUMN_NAME, ORDINAL_POSITION, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name = tableName;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getStoredProcedureParameters
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getStoredProcedureParameters`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getStoredProcedureParameters` 
(
    IN procedureName varchar(128)
)
DETERMINISTIC
BEGIN
    
    SELECT PARAMETER_NAME, ORDINAL_POSITION, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, PARAMETER_MODE
        FROM information_schema.parameters 
        WHERE SPECIFIC_NAME = procedureName;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure addBookToBookInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`addBookToBookInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `addBookToBookInfo`
(
    IN authorKey INT,
    IN titleKey INT,
    IN genreKey INT,
    IN formatKey INT,
    IN seriesKey INT,
    OUT bookKey INT
)
DETERMINISTIC
BEGIN
    
    INSERT INTO bookinfo (bookinfo.AuthorFKbi, bookinfo.TitleFKbi, bookinfo.CategoryFKbi, bookinfo.BookFormatFKbi, bookinfo.SeriesFKbi)
        VALUES (authorKey, titleKey, genreKey, formatKey, seriesKey);
    SET bookKey := LAST_INSERT_ID();

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure getBookInfo
-- -----------------------------------------------------

USE `pacswlibinvtool`;
DROP procedure IF EXISTS `pacswlibinvtool`.`getBookInfo`;

DELIMITER $$
USE `pacswlibinvtool`$$
CREATE PROCEDURE `getBookInfo` 
(
    IN bookKey INT
)
DETERMINISTIC
BEGIN
    
    IF bookKey > 0 THEN
        SELECT * FROM bookinfo WHERE idBookInfo = bookKey;
    ELSE
        SELECT * FROM bookinfo;
    END IF;

END$$

DELIMITER ;

