-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: pacswlibinvtool
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `authorstab`
--

DROP TABLE IF EXISTS `authorstab`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `authorstab` (
  `idAuthors` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LastName` varchar(20) NOT NULL,
  `FirstName` varchar(20) NOT NULL,
  `MiddleName` varchar(20) DEFAULT NULL,
  `YearOfBirth` varchar(4) DEFAULT NULL,
  `YearOfDeath` varchar(4) DEFAULT NULL,
  PRIMARY KEY (`idAuthors`,`LastName`,`FirstName`),
  UNIQUE KEY `idAuthors_UNIQUE` (`idAuthors`),
  KEY `LastName` (`LastName`),
  KEY `LastCMFirst` (`LastName`,`FirstName`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `authorstab`
--

LOCK TABLES `authorstab` WRITE;
/*!40000 ALTER TABLE `authorstab` DISABLE KEYS */;
INSERT INTO `authorstab` VALUES (1,'Heinlein','Robert','Anson','1907','1988'),(2,'Asimov','Isaac',NULL,'1920','1992'),(3,'Clarke','Arthur','Charles','1917','2008'),(4,'Le Guin','Ursula','Kroeber','1929','2018'),(5,'Bradbury','Ray','Douglas ','1920','2012'),(6,'Dick','Philip','Kindred','1928','1982'),(7,'Wells','Herbert','George','1866','1946'),(8,'Silverberg','Robert',NULL,'1935',NULL),(9,'Zimmer Bradley','Marion','Eleanor','1930','1999'),(10,'Norton','Andre','Alice','1912','2005'),(11,'Drake','David',NULL,'1945',NULL),(12,'Weber','David','Mark','1952',NULL),(13,'Baxter','Stephen',NULL,'1957',NULL),(15,'Brin','David','Glen','1950',NULL),(16,'Oneal','Bill',NULL,'1977',NULL),(17,'Herbert','Frank','Patrick','1920','1986'),(18,'Herbert','Brian','Patrick','1967',NULL),(19,'Anthony','Piers',NULL,'1934',NULL),(20,'Stevenson','Robert','Louis','1850','1894'),(21,'Wright','Frank','Lloyd','1867','1959'),(22,'Bradley','Omar','Nelson','1893','1981'),(23,'Chernick','Goldie','Pinch','1925','2008'),(24,'Alcot','Louisa','May','1832','1888'),(25,'Braun','Wernher','Von','1912','1977'),(26,'Chernick','Isadore','Ike','1923','1999'),(27,'Pease','Howard','A','1894','1974'),(28,'Norman','John',NULL,'1931',NULL);
/*!40000 ALTER TABLE `authorstab` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bkconditions`
--

DROP TABLE IF EXISTS `bkconditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bkconditions` (
  `idBkConditions` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Primary key of Book Conditions table.\nBook Conditions are Excellent, Good, Fair, Poor. They are used to partially describe the condition of the book.\nThis table will be used to create a dictionary of book conditions in a UI/Tool.',
  `ConditionOfBookStr` varchar(16) NOT NULL COMMENT 'This string will contain 1 of 4 conditions for the book, Execellent, Good, Fair or Poor. This is a rating of the physical condition of the book.',
  PRIMARY KEY (`idBkConditions`),
  UNIQUE KEY `idbkconditions_UNIQUE` (`idBkConditions`),
  UNIQUE KEY `ConditionOfBook_UNIQUE` (`ConditionOfBookStr`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bkconditions`
--

LOCK TABLES `bkconditions` WRITE;
/*!40000 ALTER TABLE `bkconditions` DISABLE KEYS */;
INSERT INTO `bkconditions` VALUES (2,'Excellent'),(4,'Fair'),(3,'Good'),(1,'Not In Library'),(5,'Poor');
/*!40000 ALTER TABLE `bkconditions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bkstatuses`
--

DROP TABLE IF EXISTS `bkstatuses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bkstatuses` (
  `idBkStatus` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Primary Key of the book status. \nBook statuses are New or Usedr. They are used to partially describe the condition of the book.\nThis table will be used to create a dictionary of book conditions in a UI/Tool.',
  `BkStatusStr` varchar(45) DEFAULT NULL COMMENT 'This string will contain one of two values, either New or Used.',
  PRIMARY KEY (`idBkStatus`),
  UNIQUE KEY `idbkstatus_UNIQUE` (`idBkStatus`),
  UNIQUE KEY `BkStatusStr_UNIQUE` (`BkStatusStr`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bkstatuses`
--

LOCK TABLES `bkstatuses` WRITE;
/*!40000 ALTER TABLE `bkstatuses` DISABLE KEYS */;
INSERT INTO `bkstatuses` VALUES (2,'New'),(1,'Not In Library'),(3,'Used');
/*!40000 ALTER TABLE `bkstatuses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bksynopsis`
--

DROP TABLE IF EXISTS `bksynopsis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bksynopsis` (
  `BookFKsyop` int(10) unsigned NOT NULL,
  `StoryLine` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`BookFKsyop`),
  KEY `BookFKsYnop` (`BookFKsyop`),
  CONSTRAINT `BookInfoFKSynopsis` FOREIGN KEY (`BookFKsyop`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bksynopsis`
--

LOCK TABLES `bksynopsis` WRITE;
/*!40000 ALTER TABLE `bksynopsis` DISABLE KEYS */;
INSERT INTO `bksynopsis` VALUES (1,'bookDescription'),(5,'Testing 1 2 3'),(6,'The start of the Great Wall of Northland.'),(7,'The Golden Age of Northland');
/*!40000 ALTER TABLE `bksynopsis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookcategories`
--

DROP TABLE IF EXISTS `bookcategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bookcategories` (
  `idBookCategories` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(45) NOT NULL COMMENT 'This will be strings like Non-Fiction, Mystery, Science-Fiction, Fantasy, Poetry, Art etc.',
  PRIMARY KEY (`idBookCategories`,`CategoryName`),
  UNIQUE KEY `idBookCategories_UNIQUE` (`idBookCategories`),
  KEY `CategoryNames` (`CategoryName`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookcategories`
--

LOCK TABLES `bookcategories` WRITE;
/*!40000 ALTER TABLE `bookcategories` DISABLE KEYS */;
INSERT INTO `bookcategories` VALUES (1,'Non-Fiction'),(2,'Non-Fiction: Biography'),(3,'Non-Fiction: Biology'),(4,'Non-Fiction: Computer'),(5,'Non-Fiction: Electrical Engineering'),(6,'Non-Fiction: History'),(7,'Textbook'),(8,'Poetry'),(9,'Art'),(10,'Dictionary'),(11,'Encyclopedia'),(12,'Fiction'),(13,'Fiction: Anime'),(14,'Fiction: Fantasy'),(15,'Fiction: Horror'),(16,'Fiction: Romance'),(17,'Fiction: Science Fiction'),(18,'Fiction: Western'),(19,'Non-Fiction: Chemistry');
/*!40000 ALTER TABLE `bookcategories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookcondition`
--

DROP TABLE IF EXISTS `bookcondition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bookcondition` (
  `BookFKCond` int(10) unsigned NOT NULL COMMENT 'BookFKCond is a foreign key into the bookinfo table. There will be one bookcondition for each book in the library.',
  `ConditionOfBook` int(10) unsigned NOT NULL COMMENT 'This is a foreign key into the bkconditions table. It will be an integer between 1 and 4 representing the condition of the book.',
  `NewOrUsed` int(10) unsigned NOT NULL COMMENT 'This integer is a foreign key into the bkstatuses table. It will have a value of either 1 or 2.',
  `PhysicalDescriptionStr` varchar(256) DEFAULT NULL COMMENT 'The user can provide a brief summary of the books condition.',
  `IsSignedByAuthor` tinyint(4) NOT NULL COMMENT 'Boolean value, has the author signed the book or not. Replaces the signedbyauthor table in the previous version of the database.',
  `BookHasBeenRead` tinyint(4) NOT NULL COMMENT 'Boolean Value, has the user read the book.',
  PRIMARY KEY (`BookFKCond`),
  UNIQUE KEY `BookFKCond_UNIQUE` (`BookFKCond`),
  KEY `IsReadIndex` (`BookHasBeenRead`),
  KEY `IsSignedIndex` (`IsSignedByAuthor`),
  KEY `statusindexfk_idx` (`NewOrUsed`),
  KEY `conditionindexfk_idx` (`ConditionOfBook`),
  CONSTRAINT `condbookinfoidxfk` FOREIGN KEY (`BookFKCond`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE,
  CONSTRAINT `conditionindexfk` FOREIGN KEY (`ConditionOfBook`) REFERENCES `bkconditions` (`idbkconditions`),
  CONSTRAINT `statusindexfk` FOREIGN KEY (`NewOrUsed`) REFERENCES `bkstatuses` (`idbkstatus`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookcondition`
--

LOCK TABLES `bookcondition` WRITE;
/*!40000 ALTER TABLE `bookcondition` DISABLE KEYS */;
INSERT INTO `bookcondition` VALUES (1,3,3,'Cover bent.',0,1),(3,3,3,'Cover bent.',0,1),(4,3,3,'Cover bent.',0,1),(5,2,2,NULL,0,0),(6,2,2,'New book, what could be wrong?',0,1),(7,2,2,NULL,0,1),(8,2,2,NULL,0,0),(12,3,3,'Dust Jacket Dusty',1,1);
/*!40000 ALTER TABLE `bookcondition` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookformat`
--

DROP TABLE IF EXISTS `bookformat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bookformat` (
  `idFormat` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FormatName` varchar(45) NOT NULL,
  PRIMARY KEY (`idFormat`,`FormatName`),
  UNIQUE KEY `idFormat_UNIQUE` (`idFormat`),
  UNIQUE KEY `FormatName_UNIQUE` (`FormatName`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookformat`
--

LOCK TABLES `bookformat` WRITE;
/*!40000 ALTER TABLE `bookformat` DISABLE KEYS */;
INSERT INTO `bookformat` VALUES (1,'Not In Library'),(2,'Hardcover'),(3,'Trade Paperback'),(4,'Mass Market Paperback'),(5,'eBook PDF'),(6,'eBook Kindle'),(7,'eBook iBooks'),(8,'eBook EPUB'),(9,'eBook HTML');
/*!40000 ALTER TABLE `bookformat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bookinfo`
--

DROP TABLE IF EXISTS `bookinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `bookinfo` (
  `idBookInfo` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TitleFKbi` int(10) unsigned NOT NULL,
  `AuthorFKbi` int(10) unsigned NOT NULL COMMENT 'Foreign Key Into Author Table',
  `CategoryFKbi` int(10) unsigned NOT NULL,
  `BookFormatFKbi` int(10) unsigned NOT NULL COMMENT 'Foreign Key Into Format Table',
  `SeriesFKBi` int(10) unsigned NOT NULL COMMENT 'Foreign Key into Series Table',
  PRIMARY KEY (`idBookInfo`,`TitleFKbi`,`AuthorFKbi`),
  UNIQUE KEY `idBookInfo_UNIQUE` (`idBookInfo`),
  KEY `CategoryFKbI` (`CategoryFKbi`),
  KEY `AuthorFKbi` (`AuthorFKbi`),
  KEY `BookFormatFKBi` (`BookFormatFKbi`),
  KEY `SeriesFKBi` (`SeriesFKBi`),
  KEY `TitleFKbi` (`TitleFKbi`),
  CONSTRAINT `BkAuthorBookFK` FOREIGN KEY (`AuthorFKbi`) REFERENCES `authorstab` (`idauthors`) ON DELETE CASCADE,
  CONSTRAINT `BkCatBookFK` FOREIGN KEY (`CategoryFKbi`) REFERENCES `bookcategories` (`idbookcategories`),
  CONSTRAINT `BkFormatBookFK` FOREIGN KEY (`BookFormatFKbi`) REFERENCES `bookformat` (`idformat`),
  CONSTRAINT `TitleBookFK` FOREIGN KEY (`TitleFKbi`) REFERENCES `title` (`idtitle`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bookinfo`
--

LOCK TABLES `bookinfo` WRITE;
/*!40000 ALTER TABLE `bookinfo` DISABLE KEYS */;
INSERT INTO `bookinfo` VALUES (1,1,12,17,4,2),(3,3,12,17,4,2),(4,4,12,17,4,2),(5,5,10,17,4,0),(6,6,13,17,4,6),(7,7,13,17,4,6),(8,8,13,17,4,6),(12,12,15,17,2,0);
/*!40000 ALTER TABLE `bookinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `forsale`
--

DROP TABLE IF EXISTS `forsale`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `forsale` (
  `BookFKfs` int(10) unsigned NOT NULL,
  `IsForSale` tinyint(4) NOT NULL DEFAULT '0',
  `AskingPrice` double NOT NULL DEFAULT '0',
  `EstimatedValue` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`BookFKfs`),
  UNIQUE KEY `BookFKfs_UNIQUE` (`BookFKfs`),
  KEY `BookFKfs` (`BookFKfs`),
  CONSTRAINT `fsBookFK` FOREIGN KEY (`BookFKfs`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `forsale`
--

LOCK TABLES `forsale` WRITE;
/*!40000 ALTER TABLE `forsale` DISABLE KEYS */;
INSERT INTO `forsale` VALUES (1,0,8.99,8.99),(3,0,6.99,6.99),(4,0,7.99,7.99),(5,0,6.99,6.99),(6,0,6.99,6.99),(7,0,8.99,8.99),(8,0,6.99,6.99),(12,0,100,100);
/*!40000 ALTER TABLE `forsale` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `owned`
--

DROP TABLE IF EXISTS `owned`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `owned` (
  `BookFKo` int(10) unsigned NOT NULL,
  `IsOwned` tinyint(4) NOT NULL,
  `IsWishListed` tinyint(4) NOT NULL,
  PRIMARY KEY (`BookFKo`),
  KEY `BookFKo` (`BookFKo`),
  KEY `ownedindex` (`IsOwned`),
  KEY `wishindex` (`IsWishListed`),
  CONSTRAINT `ownedBookFK` FOREIGN KEY (`BookFKo`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `owned`
--

LOCK TABLES `owned` WRITE;
/*!40000 ALTER TABLE `owned` DISABLE KEYS */;
INSERT INTO `owned` VALUES (1,1,0),(3,1,0),(4,1,0),(5,1,0),(6,1,0),(7,1,0),(8,1,0),(12,1,0);
/*!40000 ALTER TABLE `owned` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `publishinginfo`
--

DROP TABLE IF EXISTS `publishinginfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `publishinginfo` (
  `BookFKPubI` int(10) unsigned NOT NULL COMMENT 'Foreign Key into the Book Info Table.',
  `ISBNumber` varchar(32) DEFAULT NULL COMMENT 'This was previously the only data field in the isbn table, it has been moved here and the isbn table has been removed.',
  `Copyright` varchar(4) NOT NULL,
  `Edition` int(10) unsigned DEFAULT NULL,
  `Printing` int(10) unsigned DEFAULT NULL COMMENT 'A book may be printed may times. This will indicate which printing it is. Check the back of the title page.',
  `Publisher` varchar(45) DEFAULT NULL,
  `OutOfPrint` tinyint(4) DEFAULT NULL COMMENT 'Is the book still being printed or has it lapsed.',
  PRIMARY KEY (`BookFKPubI`),
  UNIQUE KEY `ISBNumber_UNIQUE` (`ISBNumber`),
  KEY `BookFKPubI` (`BookFKPubI`),
  KEY `ISBNindex` (`ISBNumber`),
  CONSTRAINT `bookDataFKpub` FOREIGN KEY (`BookFKPubI`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `publishinginfo`
--

LOCK TABLES `publishinginfo` WRITE;
/*!40000 ALTER TABLE `publishinginfo` DISABLE KEYS */;
INSERT INTO `publishinginfo` VALUES (1,'0-7434-3571-0','1993',1,9,'Baen Books',0),(3,'0-7434-3573-7','1994',1,8,'Baen Books',0),(4,'0-7434-3574-5','1994',1,6,'Baen Books',0),(5,'978-0-345-35036-7','1955',3,4,'Harcourt',0),(6,'978-0-451-46446-0','2010',1,1,'Roc',0),(7,'978-0-451-41486-1','2011',1,1,'Roc',0),(8,'978-0-451-41919-4','2012',1,1,'Roc',0),(12,'0-932096-44-1','1987',1,1,'Phantasia Press',0);
/*!40000 ALTER TABLE `publishinginfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchaseinfo`
--

DROP TABLE IF EXISTS `purchaseinfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `purchaseinfo` (
  `BookFKPurI` int(10) unsigned NOT NULL,
  `PurchaseDate` date DEFAULT NULL,
  `ListPrice` double DEFAULT NULL,
  `PaidPrice` double DEFAULT NULL,
  `Vendor` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`BookFKPurI`),
  KEY `BookFKPurI` (`BookFKPurI`),
  KEY `DateBoughtIndex` (`PurchaseDate`),
  CONSTRAINT `purBookFK` FOREIGN KEY (`BookFKPurI`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchaseinfo`
--

LOCK TABLES `purchaseinfo` WRITE;
/*!40000 ALTER TABLE `purchaseinfo` DISABLE KEYS */;
INSERT INTO `purchaseinfo` VALUES (5,'2018-12-06',7.99,7.99,'Amazon'),(6,'2018-12-06',7.99,7.19,'Barnes & Noble'),(7,'2018-12-06',9.99,8.99,'Barnes & Noble'),(8,'2018-12-06',7.99,7.19,'Barnes & Noble');
/*!40000 ALTER TABLE `purchaseinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ratings`
--

DROP TABLE IF EXISTS `ratings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `ratings` (
  `BookFKRats` int(10) unsigned NOT NULL COMMENT 'Primary Key, but also foreign key to the bookinfo table.',
  `MyRatings` double DEFAULT NULL COMMENT '1 to 5 star rating based on my feelings about the book.',
  `AmazonRatings` double DEFAULT NULL COMMENT '1 to 5 star rating from Amazon.com',
  `GoodReadsRatings` double DEFAULT NULL COMMENT '1 to 5 star rating from the GoodReads.com website',
  PRIMARY KEY (`BookFKRats`),
  UNIQUE KEY `BookFKRats_UNIQUE` (`BookFKRats`),
  KEY `MyRatings_idx` (`MyRatings`),
  KEY `AmazonRatings_idx` (`AmazonRatings`),
  KEY `GoodReadsRats_idx` (`GoodReadsRatings`),
  CONSTRAINT `BookFKRats` FOREIGN KEY (`BookFKRats`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ratings`
--

LOCK TABLES `ratings` WRITE;
/*!40000 ALTER TABLE `ratings` DISABLE KEYS */;
INSERT INTO `ratings` VALUES (1,4.1,4.5,4.12),(3,4.5,4.5,4.17),(4,4.6,4.6,4.2),(12,4.7,4.4,4.06);
/*!40000 ALTER TABLE `ratings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `series`
--

DROP TABLE IF EXISTS `series`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `series` (
  `idSeries` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AuthorOfSeries` int(10) unsigned NOT NULL COMMENT 'Foriegn Key into Author Table',
  `SeriesName` varchar(128) NOT NULL,
  PRIMARY KEY (`idSeries`,`AuthorOfSeries`,`SeriesName`),
  UNIQUE KEY `idSeries_UNIQUE` (`idSeries`),
  KEY `AuthorFKs` (`AuthorOfSeries`),
  KEY `SeriesTitle` (`SeriesName`),
  CONSTRAINT `authorfksidx` FOREIGN KEY (`AuthorOfSeries`) REFERENCES `authorstab` (`idauthors`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `series`
--

LOCK TABLES `series` WRITE;
/*!40000 ALTER TABLE `series` DISABLE KEYS */;
INSERT INTO `series` VALUES (1,12,'Safehold'),(2,12,'Honor Harrington'),(3,12,'Honorverse'),(4,9,'Darkover'),(5,2,'Foundation'),(6,13,'Northland'),(8,15,'The Uplift Saga'),(9,16,'Loans for You'),(10,17,'Dune'),(11,18,'Dune'),(12,5,'R is for Rocket'),(13,17,'Dosadi Experiment');
/*!40000 ALTER TABLE `series` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `title`
--

DROP TABLE IF EXISTS `title`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `title` (
  `idTitle` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `TitleStr` varchar(128) NOT NULL,
  PRIMARY KEY (`idTitle`,`TitleStr`),
  UNIQUE KEY `idTitle_UNIQUE` (`idTitle`),
  KEY `TitleStr` (`TitleStr`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `title`
--

LOCK TABLES `title` WRITE;
/*!40000 ALTER TABLE `title` DISABLE KEYS */;
INSERT INTO `title` VALUES (1,'On Basilisk Station'),(2,'Honor of the Queen'),(3,'Short Victorious War'),(4,'Field of Dishonor'),(5,'Star Guard'),(6,'Stone Spring'),(7,'Bronze Summer'),(8,'Iron Winter'),(9,'Fundamental Algorithms'),(10,'Seminumerical Algorithms'),(11,'Sorting and Searching'),(12,'Uplift War');
/*!40000 ALTER TABLE `title` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `volumeinseries`
--

DROP TABLE IF EXISTS `volumeinseries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `volumeinseries` (
  `BookFKvs` int(10) unsigned NOT NULL,
  `SeriesFK` int(10) unsigned NOT NULL,
  `VolumeNumber` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`BookFKvs`),
  KEY `BookFKvsidx` (`BookFKvs`),
  KEY `SeriesFKvsidx` (`SeriesFK`),
  CONSTRAINT `BookInfoFKvolumeS` FOREIGN KEY (`BookFKvs`) REFERENCES `bookinfo` (`idbookinfo`) ON DELETE CASCADE,
  CONSTRAINT `SeriesFKVolumeS` FOREIGN KEY (`SeriesFK`) REFERENCES `series` (`idseries`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `volumeinseries`
--

LOCK TABLES `volumeinseries` WRITE;
/*!40000 ALTER TABLE `volumeinseries` DISABLE KEYS */;
INSERT INTO `volumeinseries` VALUES (1,2,1),(3,2,3),(4,2,4),(6,6,1),(7,6,2),(8,6,3);
/*!40000 ALTER TABLE `volumeinseries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'pacswlibinvtool'
--
/*!50003 DROP FUNCTION IF EXISTS `findAuthorKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findAuthorKey`(
    firstName VARCHAR(20),
    lastName VARCHAR(20)
) RETURNS int(11)
    DETERMINISTIC
BEGIN
    
    SET @authorKey = 0;
    
    SELECT COUNT(*) INTO @authorCount FROM authorstab;
    IF @authorCount > 0 THEN
        SELECT authorstab.idAuthors INTO @authorKey
            FROM authorstab
            WHERE authorsTab.LastName = lastName AND authorsTab.FirstName = firstName;
        IF @authorKey IS NULL THEN
            SET @authorKey = 0;
        END IF;
    END IF;

    RETURN @authorKey;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findbkConditionKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findbkConditionKey`(
    bkConditionStr VARCHAR(20)
) RETURNS int(11)
    DETERMINISTIC
BEGIN
    
    SET @bkConditionKey = 0;
    
    SELECT bkconditions.idBkConditions INTO @bkConditionKey
        FROM bkconditions
        WHERE bkconditions.ConditionOfBookStr = bkConditionStr;
    IF @bkConditionKey IS NULL THEN
        SET @bkConditionKey = 0;
    END IF;

    RETURN @bkConditionKey;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findbkStatusKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findbkStatusKey`(
    bkStatusStr VARCHAR(20)
) RETURNS int(11)
    DETERMINISTIC
BEGIN
    
    SET @bkStatusKey = 0;
    
    SELECT bkstatuses.idBkStatus INTO @bkStatusKey
        FROM bkstatuses
        WHERE bkstatuses.BkStatusStr = bkStatusStr;
    IF @bkStatusKey IS NULL THEN
        SET @bkStatusKey = 0;
    END IF;

    RETURN @bkStatusKey;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findBookKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findBookKey`(
    authorLast VARCHAR(20),
    authorFirst VARCHAR(20),
    titleStr VARCHAR(128),
    formatStr VARCHAR(45)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @bookKey = 0;
    
    SET @authorKey = findauthorKey(authorFirst, authorLast);
    
    SET @titleKey = findTitleKey(titleStr);
    
    SET @formatKey = findFormatKeyFromStr(formatStr);
    
    IF @authorKey > 0 AND @titleKey > 0 THEN
        SET @bookKey = findBookKeyFromKeys(@authorKey, @titleKey, @formatKey);
    END IF;
    
    RETURN @bookKey;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findBookKeyFast` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findBookKeyFast`(
    authorLast VARCHAR(20),
    authorFirst VARCHAR(20),
    titleStr VARCHAR(128),
    formatStr VARCHAR(45)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    /*
     * There may be multiple copies of a book in the library, one of each format.
     * Specifying the format makes it distinct.
     */

    SELECT BKI.idBookInfo INTO @bookKey FROM bookinfo as BKI
        INNER JOIN authorsTab AS a ON a.idAuthors = BKI.AuthorFKbi
        INNER JOIN title AS t ON t.idTitle = BKI.TitleFKbi
        INNER JOIN bookformat AS bf ON bf.idFormat = BKI.BookFormatFKBi
        WHERE a.LastName = authorLast AND a.FirstName = authorFirst AND t.TitleStr = titleStr and bf.FormatName = formatStr;

    IF @bookKey IS NULL THEN
        SET @bookKey = 0;
    END IF;

    
    RETURN @bookKey;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findBookKeyFromKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findBookKeyFromKeys`(
    authorKey INT,
    titleKey INT,
    formatKey INT
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @bookKey = 0;
    
    IF authorKey > 0 AND titleKey > 0 then
        SELECT bookinfo.idBookInfo INTO @bookKey 
            FROM BookInfo 
            WHERE bookinfo.AuthorFKbi = authorKey AND bookinfo.TitleFKbi = titleKey AND bookinfo.BookFormatFKbi = formatKey;
        IF @bookKey IS NULL THEN
            SET @bookKey = 0;
        END IF;
    END IF;
    
    RETURN @bookKey;
        
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findCategoryKeyFromStr` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findCategoryKeyFromStr`(
    categoryName VARCHAR(45)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @categoryKey = 0;

    SELECT COUNT(*) INTO @categoryCount FROM bookcategories;
    IF @categoryCount > 0 THEN
        SELECT bookcategories.idBookCategories INTO @categoryKey
            FROM bookcategories
            WHERE bookcategories.CategoryName = categoryName;
        IF @categoryKey IS NULL THEN
            SET @categoryKey = 0;
        END IF;
    END IF;

    RETURN @categoryKey;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findFormatKeyFromStr` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findFormatKeyFromStr`(
    bookFormatStr VARCHAR(45)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @formatKey = 0;
    
    SELECT COUNT(*) INTO @formatCount FROM bookformat;
    IF @formatCount > 0 THEN
        SELECT bookformat.idFormat INTO @formatKey
            FROM bookformat
            WHERE bookformat.FormatName = bookFormatStr;
        IF @formatKey IS NULL THEN
            SET @formatKey = 0;
        END IF;
    END IF;
    
    RETURN @formatKey;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findSeriesKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findSeriesKey`(
    authorFirst VARCHAR(20),
    authorLast VARCHAR(20),
    seriesTitle VARCHAR(128)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @authorKey = findAuthorKey(authorFirst, authorLast);
    
    SET @seriesKey = findSeriesKeyByAuthKeyTitle(@authorKey, seriesTitle);
        
    RETURN @seriesKey;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findSeriesKeyByAuthKeyTitle` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findSeriesKeyByAuthKeyTitle`(
    authorKey INT,
    seriesTitle VARCHAR(128)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @seriesKey = 0;
        
    IF authorKey > 0 THEN
        SELECT series.idSeries INTO @seriesKey FROM series WHERE series.AuthorOfSeries = authorKey AND series.SeriesName = seriesTitle;
        IF @seriesKey IS NULL THEN
            SET @seriesKey = 0;
        END IF;
    END IF;
    
    RETURN @seriesKey;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `findTitleKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `findTitleKey`(
    TitleStr VARCHAR(128)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SELECT title.idTitle INTO @titleKey FROM title WHERE title.TitleStr = TitleStr;
    IF @titleKey IS NULL THEN
        SET @titleKey = 0;
    END IF;
    
    RETURN @titleKey;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `insertTitleIfNotExist` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `insertTitleIfNotExist`(
    titleStr VARCHAR(128)
) RETURNS int(11)
    DETERMINISTIC
BEGIN

    SET @titleKey = findTitleKey(titleStr);

    if @titleKey < 1 THEN
        INSERT INTO title (title.TitleStr) VALUES(titleStr);
        SET @titleKey := LAST_INSERT_ID();
    END IF;
    
    RETURN @titleKey;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addAuthor`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addAuthorSeries` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addAuthorSeries`(
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

    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addAuthorSeriesWithAuthorKey` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addAuthorSeriesWithAuthorKey`(
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

    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addBookToLibrary` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addBookToLibrary`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addCategory` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addCategory`(
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addFormat` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addFormat`(IN bookFormatStr VARCHAR(45), OUT primaryKey INT)
    DETERMINISTIC
BEGIN

    SET @formatKey = findFormatKeyFromStr(bookFormatStr);
    
    -- Prevent adding the same format again to avoid breaking the unique key structure.
    IF @formatKey < 1 THEN
        INSERT INTO bookformat (bookformat.FormatName) VALUES(bookFormatStr);
        SET primaryKey := LAST_INSERT_ID();
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addMoreBooksForInterst` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addMoreBooksForInterst`()
    DETERMINISTIC
BEGIN

    DECLARE bookKey INT;
SET @procName = 'addMoreBooksForInterst';

    -- These 3 books are not included in the previous testing.
    CALL addBookToLibrary('Non-Fiction: Computer', 'Knuth', 'Donald', 'Fundamental Algorithms',  'Hardcover', '1973', '0-201-03809-9', 2, NULL, 'Addison Wesley', 0,
        'The Art of Computer Programming', 1, 'Used', 'Good', 'No dust jacket, like new.', 0, 1, 1, 0, 0.00, 0.00, 1, NULL, NULL, NULL, NULL, bookKey);
    IF bookKey = 0 THEN
        SET @emsg = 'Failed to add Fundamental Algorithms';
        SELECT emsg;
    END IF;
    CALL addBookToLibrary('Non-Fiction: Computer', 'Knuth', 'Donald', 'Seminumerical Algorithms',  'Hardcover', '1981', '0-201-03822-6', 2, NULL, 'Addison Wesley', 0,
	'The Art of Computer Programming', 2, 'Used', 'Good', 'No dust jacket, like new.', 0, 1, 1, 0, 0.00, 0.00, 1, NULL, NULL, NULL, NULL, bookKey);
    IF bookKey = 0 THEN
        SET @emsg = 'Failed to add Seminumerical Algorithms';
        SELECT emsg;
    END IF;
    CALL addBookToLibrary('Non-Fiction: Computer', 'Knuth', 'Donald', 'Sorting and Searching',  'Hardcover', '1973', '0-201-03803-X', 2, NULL, 'Addison Wesley', 0, 
        'The Art of Computer Programming', 3, 'Used', 'Good', 'No dust jacket, like new.', 0, 1, 1, 0, 0.00, 0.00, 1, NULL, NULL, NULL, NULL, bookKey);
    IF bookKey = 0 THEN
        SET @emsg = 'Failed to add Sorting and Searching';
        SELECT emsg;
        SELECT * from authorstab;
        SELECT * from series;
    END IF;
    CALL addAuthor('Brin', 'David', 'Glen', '1950', NULL, bookKey);
    CALL addAuthorSeries('David', 'Brin', 'The Uplift Saga');
    CALL addBookToLibrary('Fiction: Science Fiction', 'Brin', 'David', 'Uplift War', 'HardCover', '1987', '0-932096-44-1', 1, 1, 'Phantasia Press', 0,
        NULL, NULL, 'Used', 'Good', 'Dust Jacket Dusty', 1, 1, 1, 0, 0, 100.00, 100.00, NULL, NULL, NULL, NULL, bookKey);
    CALL rateThisBook('David', 'Brin', 'Uplift War', 'HardCover', 4.7, 4.4, 4.06);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `addOrUpdateBookDataInTables` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `addOrUpdateBookDataInTables`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `bookSold` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `bookSold`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `buyBook` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `buyBook`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `deleteAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteAuthor`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `deleteBook` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteBook`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `finishedReadingBook` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `finishedReadingBook`(
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllAuthorsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllAuthorsData`()
    DETERMINISTIC
BEGIN

/*
 * Could be used to create a select control of authors for a book or possibly a typing completion text control.
 */

    SELECT * FROM authorstab
        ORDER BY authorstab.LastName, authorstab.FirstName, authorstab.MiddleName;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBookCategoriesWithKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBookCategoriesWithKeys`()
    DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the categories to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bookcategories.CategoryName, bookcategories.idBookCategories FROM bookcategories;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBookDataSortedByMyRatings` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBookDataSortedByMyRatings`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBookFormatsWithKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBookFormatsWithKeys`()
    DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the formats to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bookformat.FormatName, bookformat.idFormat FROM bookformat;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooks`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksByThisAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksByThisAuthor`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksForSale` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksForSale`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksInCategory` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksInCategory`(IN categoryKey INT)
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksInCategorySortedByMyRatings` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksInCategorySortedByMyRatings`(IN categoryKey INT)
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksNoKeysInLib` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksNoKeysInLib`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksSignedByAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksSignedByAuthor`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksThatWereRead` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksThatWereRead`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksWithKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksWithKeys`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllBooksWithKeysInLib` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllBooksWithKeysInLib`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllConditionsWithKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllConditionsWithKeys`()
    DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the conditions to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bkconditions.ConditionOfBookStr, bkconditions.idBkConditions FROM bkconditions;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllSeriesByThisAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllSeriesByThisAuthor`(
    IN AuthorLastName VARCHAR(20),
    IN AuthorFirstName VARCHAR(20)
)
    DETERMINISTIC
BEGIN

    SELECT series.SeriesName, series.idSeries FROM series WHERE series.AuthorOfSeries = findauthorKey(AuthorFirstName, AuthorLastName);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllSeriesData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllSeriesData`()
    DETERMINISTIC
BEGIN

    SELECT a.LastName, a.FirstName, s.SeriesName
        FROM series AS s
        INNER JOIN authorstab AS a
        ON a.idAuthors = s.AuthorOfSeries
        ORDER BY a.LastName, a.FirstName, s.SeriesName;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllStatusesWithKeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllStatusesWithKeys`()
    DETERMINISTIC
BEGIN

/*
 * Example usage would be to get all the statuses to CREATE a control that embeds the primary key rather than the text.
 */

    SELECT bkstatuses.BkStatusStr, bkstatuses.idBkStatus FROM bkstatuses;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAllWishListBooks` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAllWishListBooks`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getAuthorDataByLastName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getAuthorDataByLastName`(
    IN authorLastName VARCHAR(20)
)
    DETERMINISTIC
BEGIN
    
    /*
     * Will return multiple authors with the same last name, e.g. if the last name is Herbert
     * it will return both Frank Herbert and Brian Herbert.
     */
    SELECT * FROM authorstab WHERE authorstab.LastName = authorLastName;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getBookData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getBookData`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getPublishingInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getPublishingInfo`(
    IN bookKey INT
)
    DETERMINISTIC
BEGIN

	IF bookKey > 0 THEN
		SELECT * FROM publishinginfo
			WHERE BookFKPubI = bookKey;
	ELSE
		SELECT  * FROM publishinginfo;
	END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getRatedBooksSortedByAmazonRating` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getRatedBooksSortedByAmazonRating`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getRatedBooksSortedByGoodReadsRating` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getRatedBooksSortedByGoodReadsRating`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getRatedBooksSortedByMyRating` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getRatedBooksSortedByMyRating`()
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `getThisAuthorsData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `getThisAuthorsData`(
    IN authorLastName VARCHAR(20),
    IN authorFirstName VARCHAR(20)
)
    DETERMINISTIC
BEGIN

    SELECT * FROM authorstab WHERE authorstab.LastName = authorLastName AND authorstab.FirstName = authorFirstName;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `initBookInventoryTool` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `initBookInventoryTool`()
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateBookCondition` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateBookCondition`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateBookRatings` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateBookRatings`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateForSale` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateForSale`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateOwned` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateOwned`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdatePublishing` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdatePublishing`(
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdatePurchaseInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdatePurchaseInfo`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateSynopsis` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateSynopsis`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insertOrUpdateVolumeInSeries` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `insertOrUpdateVolumeInSeries`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `putBookUpForSale` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `putBookUpForSale`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `rateThisBook` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `rateThisBook`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateAuthor` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateAuthor`(
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

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzRunAllUnitTests` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzRunAllUnitTests`()
    DETERMINISTIC
BEGIN
    /*
     * The unit tests are in a specific order. Data from the early test procedures
     * is required by the later test procedures.
     *
     * The general functionality of the unit tests is to run the procedures or functions
     * and then test values that would be affected by the routine. If the test failed
     * then a select is run to show the error. No output means no errors.
     */

    SET @ShowAllResults = 1; -- To disable most output by queries change this to zero.

    CALL zzzUnitTestInitProcedure();
    CALL zzzUnitTestAddAuthors();
    CALL zzzUnitTestAddAuthorSeries();
    CALL zzzUnitTestAddBookToLibrary();
    CALL zzzUnitTestBuyBook();
    CALL zzzUnitTestFunctions();

    CALL addMoreBooksForInterst();
    SET @unitTestDone = 'All Unit Tests Completed! If there are any querys run before this point one or more unit tests failed.';
    SELECT @unitTestDone;

    -- Test all the data retrieval procedures to see that they return data rows.
    -- These tests by default will provide output. Visually check output, make sure every query has at least one line, check that all expected fields are showing.
    IF @showAllResults > 0 THEN
        CALL getAllBookFormatsWithKeys();
        CALL getAllBookCategoriesWithKeys();
        CALL getAllConditionsWithKeys();
        CALL getAllStatusesWithKeys();
        CALL getAllBooksNoKeysInLib(); -- Test selecting all fields except keys
        CALL getAllBooksByThisAuthor('Baxter', 'Stephen');
        CALL getAllWishListBooks();
        CALL getAllBooksThatWereRead();
        CALL getThisAuthorsData('Norton','Andre');
        CALL getAllSeriesByThisAuthor('Weber', 'David');
        CALL getAllSeriesData();
        CALL getAllBooksForSale();
        CALL getAllAuthorsData();
        CALL getBookData('Weber', 'David', 'Honor of the Queen', 'Mass Market Paperback');
        CALL getAuthorDataByLastName('Asimov'); -- This could be changed if more authors are added, such as all the Greens.
        CALL getAllBooksSignedByAuthor();
        CALL getRatedBooksSortedByMyRating();
        CALL getRatedBooksSortedByAmazonRating();
        CALL getRatedBooksSortedByGoodReadsRating();
        CALL getAllBookDataSortedByMyRatings();
        CALL getAllBooksInCategory(findCategoryKeyFromStr('Non-Fiction: Computer'));
        CALL getAllBooksInCategorySortedByMyRatings(findCategoryKeyFromStr('Fiction: Science Fiction'));
    END IF;

    CALL zzzUnitTestUserUpdates();
    CALL getAllBooks(); -- Test selecting all fields all books
    CALL zzzUnitTestDelete ();
    CALL getAllBooks(); -- Test selecting all fields all books
    CALL getAllBooksWithKeysInLib(); -- Test selecting all fields except keys
    CALL getAllBooksWithKeys(); -- Test selecting all fields except keys

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestAddAuthors` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestAddAuthors`()
    DETERMINISTIC
BEGIN

    DECLARE primaryKey INT;
SET @procName = 'zzzUnitTestAddAuthors';

    CALL addAuthor('Heinlein', 'Robert', 'Anson', '1907', '1988', primaryKey);
    CALL addAuthor('Asimov', 'Isaac', NULL, '1920', '1992', primaryKey);
    CALL addAuthor('Clarke', 'Arthur', 'Charles', '1917', '2008', primaryKey);
    CALL addAuthor('Le Guin', 'Ursula', 'Kroeber', '1929', '2018', primaryKey);
    CALL addAuthor('Bradbury', 'Ray', 'Douglas ', '1920', '2012', primaryKey);
    CALL addAuthor('Dick', 'Philip', 'Kindred', '1928', '1982', primaryKey);
    CALL addAuthor('Wells', 'Herbert', 'George', '1866', '1946', primaryKey);
    CALL addAuthor('Silverberg', 'Robert', NULL, '1935', NULL, primaryKey);
    CALL addAuthor('Zimmer Bradley', 'Marion', 'Eleanor', '1930', '1999', primaryKey);
    CALL addAuthor('Norton', 'Andre', 'Alice', '1912', '2005', primaryKey);
    CALL addAuthor('Drake', 'David', NULL, '1945', NULL, primaryKey);
    CALL addAuthor('Weber', 'David', 'Mark', '1952', NULL, primaryKey);
    CALL addAuthor('Baxter', 'Stephen', NULL, '1957', NULL, primaryKey);
    CALL addAuthor('Knuth', 'Donald', 'Ervin', '1938', NULL, primaryKey);
    
    IF (SELECT COUNT(*) FROM authorstab) != 14 THEN
        SELECT @procName, COUNT(*) FROM series;
        SELECT * FROM series;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestAddAuthorSeries` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestAddAuthorSeries`()
    DETERMINISTIC
BEGIN
SET @procName = 'zzzUnitTestAddAuthorSeries';

    CALL addAuthorSeries('David', 'Weber', 'Safehold');
    CALL addAuthorSeries('David', 'Weber', 'Honor Harrington');
    CALL addAuthorSeries('David', 'Weber', 'Honorverse');
    CALL addAuthorSeries('Marion', 'Zimmer Bradley', 'Darkover');
    CALL addAuthorSeries('Isaac', 'Asimov', 'Foundation');
    CALL addAuthorSeries('Stephen', 'Baxter', 'Northland');
    CALL addAuthorSeries('Donald', 'Knuth', 'The Art of Computer Programming');
-- The follow statement should fail to insert the series since John Ringo has not been added to authorstab.
    CALL addAuthorSeries('John', 'Ringo', 'Kildar');

    IF (SELECT COUNT(*) FROM series) != 7 THEN
        SET @procName = CONCAT(@procName, ' Expected 7 Series, got ');
        SELECT @procName, COUNT(*) FROM series;
        SELECT * FROM series;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestAddBookToLibrary` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestAddBookToLibrary`()
    DETERMINISTIC
BEGIN
/*
 * The following procedures are tested by this procedure.
 *      addBookToLibrary
 *      insertOrUpdatePublishing
 *      insertOrUpdateOwned
 *      insertOrUpdateHaveRead
 *      insertOrUpdateVolumeInSeries
 *      insertOrUpdateForSale()
 *      insertOrUpdateIsSignedByAuthor
 *      insertOrUpdateSynopsis
 *      insertOrUpdateISBN
 *      insertOrUpdatePurchaseInfo
 *
 * The following functions are tested by this procedure:
 *      findAuthorKey
 *      findFormatKeyFromStr
 *      findSeriesKeyByAuthKeyTitle
 *      insertTitleIfNotExist
 *      findCategoryKeyFromStr
 *      findBookKeyFromKeys
 *
 */

    DECLARE bookKey INT;

SET @procName = 'zzzUnitTestAddBookToLibrary';

    CALL addBookToLibrary('Fiction: Science Fiction', 'Weber', 'David', 'On Basilisk Station', 'Mass Market Paperback', '1993', '0-7434-3571-0', 1, 9, 'Baen Books', 0,
        'Honor Harrington', 1, 'Used', 'Good', 'Cover bent.', 0, 1, 1, 0, 0, 8.99, 8.99, 'bookDescription', 4.1, 4.5, 4.12, bookKey);
    IF (bookKey != 1) THEN
        SET @eMsg = 'Unable to add On Basilisk Station';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;

    CALL addBookToLibrary('Fiction: Science Fiction', 'Weber', 'David', 'Honor of the Queen', 'Mass Market Paperback', '1993', '0-7434-3572-0', 1, 10, 'Baen Books', 0,
        'Honor Harrington', 2, 'Used', 'Good', 'Cover bent.', 0, 1, 1, 0, 0, 6.99, 6.99, NULL, 4.4, 4.6, 4.21, bookKey);
    IF (bookKey != 2) THEN
        SET @eMsg = 'Unable to add Honor of the Queen';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;

    CALL addBookToLibrary('Fiction: Science Fiction', 'Weber', 'David', 'Short Victorious War', 'Mass Market Paperback', '1994', '0-7434-3573-7', 1, 8, 'Baen Books', 0,
        'Honor Harrington', 3, 'Used', 'Good', 'Cover bent.', 0, 1, 1, 0, 0, 6.99, 6.99, NULL,  4.5, 4.5, 4.17, bookKey);
    IF (bookKey != 3) THEN
        SET @eMsg = 'Unable to add Short Victorious War';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;

    CALL addBookToLibrary(
	'Fiction: Science Fiction', 'Weber', 'David', 'Field of Dishonor', 'Mass Market Paperback', '1994', '0-7434-3574-5', 1, 6, 'Baen Books', 0,
        'Honor Harrington', 4, 'Used', 'Good', 'Cover bent.', 0, 1, 1, 0, 0, 7.99, 7.99, NULL, 4.6, 4.6, 4.2, bookKey);
    IF (bookKey != 4) THEN
        SET @eMsg = 'Unable to add Field of Dishonor';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;

    CALL addBookToLibrary( 'Fiction: Science Fiction', 'Norton', 'Andre', 'Star Guard', 'Not In Library', '1955', NULL, 1, NULL, 'Harcourt', 0,
        NULL, NULL, 'Not In Library', 'Not In Library', NULL, 0, 1, 0, 1, 0, 7.99, 7.99, NULL , NULL, NULL, NULL, bookKey);
    IF (bookKey != 5) THEN
        SET @eMsg = 'Unable to Andre Norton Star Guard';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;

    -- The following statement should fail to add a book since David Brin is not in authorstab.
    -- The failure is indicated by bookKey being zero.
    CALL addBookToLibrary('Fiction: Science Fiction', 'Brin', 'David', 'Uplift War', 'Not In Library', '1987', '0-932096-44-1', 1, 1, 'Phantasia Press', 0,
        NULL, NULL, 'Used', 'Good In Library', 'Dust Jacket Dusty', 1, 1, 1, 0, 0, 100.00, 100.00, NULL, NULL, NULL, NULL, bookKey);
    IF (bookKey != 0) THEN
        SET @eMsg = 'Added David Brin Uplift War';
        SELECT @procName, bookKey, @eMsg;
        SELECT COUNT(*) FROM bookinfo;
    END IF;
    IF (SELECT COUNT(*) FROM bookinfo) != 5 THEN
        SET @eMsg = 'Expected 5 entries in bookinfo got ';
        SELECT @procName, @eMsg, COUNT(*) FROM bookinfo;
        SELECT * FROM bookInfo;
    END IF;

    IF (SELECT COUNT(*) FROM publishinginfo) != 5 THEN
        SET @eMsg = 'Expected 5 entries in publishinginfo got ';
        SELECT @procName, @emsg, COUNT(*) FROM publishinginfo;
        SELECT * FROM publishinginfo;
    END IF;

    IF (SELECT COUNT(*) FROM bksynopsis) != 1 THEN
        SET @eMsg = 'Expected 1 entries in bksynopsis got ';
        SELECT @procName, @emsg, COUNT(*) FROM bksynopsis;
        SELECT * FROM bksynopsis;
    END IF;

    IF (SELECT COUNT(*) FROM forsale) != 4 THEN
        SET @eMsg = 'Expected 4 entries in forsale got ';
        SELECT @procName, @emsg, COUNT(*) FROM forsale;
        SELECT * FROM forsale;
    END IF;

    IF (SELECT COUNT(*) FROM bookcondition) != 5 THEN
        SET @eMsg = 'Expected 5 entries in bookcondition got ';
        SELECT @procName, @emsg, COUNT(*) FROM bookcondition;
        SELECT * FROM bookcondition;
    END IF;

    IF (SELECT COUNT(*) FROM owned) != 5 THEN
        SET @eMsg = 'Expected 5 entries in owned got ';
        SELECT @procName, @emsg, COUNT(*) FROM owned;
        SELECT * FROM owned;
    END IF;

    IF (SELECT COUNT(*) FROM purchaseinfo) != 0 THEN
        SET @eMsg = 'Expected 0 entries in purchaseinfo got ';
        SELECT @procName, @emsg, COUNT(*) FROM purchaseinfo;
        SELECT * FROM purchaseinfo;
    END IF;

    IF (SELECT COUNT(*) FROM title) != 5 THEN
        SET @eMsg = 'Expected 5 entries in title got ';
        SELECT @procName, @emsg, COUNT(*) FROM title;
        SELECT * FROM title;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestBuyBook` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestBuyBook`()
    DETERMINISTIC
BEGIN
/*
 * This procedure tests the buyBook procedure. Since the buyBook procedure call addBookToLibrary, everything tested
 * by zzzUnitTestAddBookToLibrary is also tested by this procedure.
 *
 */

    DECLARE bookKey INT;
    DECLARE buyDate DATE;

SET @procName = 'zzzUnitTestBuyBook';

    Set buyDate = CURDATE();

    CALL buyBook('Fiction: Science Fiction', 'Baxter', 'Stephen', 'Stone Spring',  'Mass Market Paperback', '2010', '978-0-451-46446-0', 1, 1, 'Roc', 0,
        'Northland', 1, 'New book, what could be wrong?', 0, 'The start of the Great Wall of Northland.', buyDate, 7.99, 7.19, 'Barnes & Noble', bookKey);
    IF (bookKey != 6) THEN
        SET @eMsg = CONCAT(@procName, ' Expected value is 6');
        SELECT @eMsg, bookKey, COUNT(*) FROM bookinfo;
    END IF;

    CALL buyBook('Fiction: Science Fiction', 'Baxter', 'Stephen', 'Bronze Summer',  'Mass Market Paperback', '2011', '978-0-451-41486-1', 1, 1, 'Roc', 0,
        'Northland', 2, NULL, 0, 'The Golden Age of Northland', buyDate, 9.99, 8.99, 'Barnes & Noble', bookKey);
    IF (bookKey != 7) THEN
        SET @eMsg = CONCAT(@procName, ' Expected value is 7');
        SELECT @eMsg, bookKey, COUNT(*) FROM bookinfo;
    END IF;

    CALL buyBook('Fiction: Science Fiction', 'Baxter', 'Stephen', 'Iron Winter',  'Mass Market Paperback', '2012', '978-0-451-41919-4', 1, 1, 'Roc', 0,
        'Northland', 3, NULL, 0, NULL, buyDate, 7.99, 7.19, 'Barnes & Noble', bookKey);
    IF (bookKey != 8) THEN
        SET @eMsg = CONCAT(@procName, ' Expected value is 8');
        SELECT @eMsg, bookKey, COUNT(*) FROM bookinfo;
    END IF;

    IF (SELECT COUNT(*) FROM bookinfo) != 8 THEN
        SELECT @procName, COUNT(*) FROM bookInfo;
        SELECT * FROM bookInfo;
    END IF;

    IF (SELECT COUNT(*) FROM publishinginfo) != 8 THEN
        SELECT @procName, COUNT(*) FROM publishinginfo;
        SELECT * FROM publishinginfo;
    END IF;

    IF (SELECT COUNT(*) FROM bksynopsis) != 3 THEN
        SELECT @procName, COUNT(*) FROM bksynopsis;
        SELECT * FROM bksynopsis;
    END IF;

    IF (SELECT COUNT(*) FROM forsale) != 7 THEN
        SELECT @procName, COUNT(*) FROM forsale;
        SELECT * FROM forsale;
    END IF;

    IF (SELECT COUNT(*) FROM bookcondition) != 8 THEN
        SELECT @procName, COUNT(*) FROM bookcondition;
        SELECT * FROM bookcondition;
    END IF;

    IF (SELECT COUNT(*) FROM owned) != 8 THEN
        SELECT @procName, COUNT(*) FROM owned;
        SELECT * FROM owned;
    END IF;

    IF (SELECT COUNT(*) FROM purchaseinfo) != 3 THEN
        SELECT @procName, COUNT(*) FROM purchaseinfo;
        SELECT * FROM purchaseinfo;
    END IF;

    IF (SELECT COUNT(*) FROM title) != 8 THEN
        SELECT @procName, COUNT(*) FROM title;
        SELECT * FROM title;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestDelete` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestDelete`()
    DETERMINISTIC
BEGIN
        
    SELECT COUNT(*) INTO @bookCount FROM bookinfo;

    CALL deleteBook('Weber', 'David', 'Honor of the Queen', 'Mass Market Paperback');

    IF (SELECT COUNT(*) FROM bookinfo) != (@bookCount - 1) THEN
        SELECT * FROM bookinfo;
    END IF;
    SET @bookCount = @bookCount - 1;
    IF (SELECT COUNT(*) FROM bookcondition) > @bookCount THEN
        SELECT * FROM bookcondition;
    END IF;
    IF (SELECT COUNT(*) FROM forsale) > @bookCount THEN
        SELECT * FROM forsale;
    END IF;
    IF (SELECT COUNT(*) FROM owned) > @bookCount THEN
        SELECT * FROM owned;
    END IF;
    IF (SELECT COUNT(*) FROM purchaseinfo) > @bookCount THEN
        SELECT * FROM purchaseinfo;
    END IF;
    IF (SELECT COUNT(*) FROM publishinginfo) > @bookCount THEN
        SELECT * FROM publishinginfo;
    END IF;

    SELECT COUNT(*) INTO @bookCount FROM bookinfo;
    SELECT COUNT(*) INTO @seriesCount FROM series;
    SELECT COUNT(*) INTO @authorCount FROM authorstab;

    CALL deleteAuthor('Knuth', 'Donald', 'Ervin');

    IF (SELECT COUNT(*) FROM bookinfo) != (@bookCount - 3) THEN
        SELECT * FROM bookinfo;
    END IF;
    IF (SELECT COUNT(*) FROM series) != (@seriesCount - 1) THEN
        SELECT * FROM series;
    END IF;
    IF (SELECT COUNT(*) FROM authorstab) != (@authorsCount - 1) THEN
        SELECT * FROM authors;
    END IF;
    SET @bookCount = @bookCount - 3;
    IF (SELECT COUNT(*) FROM bookcondition) > @bookCount THEN
        SELECT * FROM bookcondition;
    END IF;
    IF (SELECT COUNT(*) FROM forsale) > @bookCount THEN
        SELECT * FROM forsale;
    END IF;
    IF (SELECT COUNT(*) FROM owned) > @bookCount THEN
        SELECT * FROM owned;
    END IF;
    IF (SELECT COUNT(*) FROM purchaseinfo) > @bookCount THEN
        SELECT * FROM purchaseinfo;
    END IF;
    IF (SELECT COUNT(*) FROM publishinginfo) > @bookCount THEN
        SELECT * FROM publishinginfo;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestFunctions` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestFunctions`()
    DETERMINISTIC
BEGIN

SET @procName = 'zzzUnitTestFunctions';

    /*
     * The functions not explicitly tested here are tested indirectly 
     * through the function calls here with the exception of insertTitleIfNotExist
     */

    SET @authorKey = findAuthorKey('Arthur','Clarke');
    IF @authorKey != 3 THEN
        SELECT @procName, @authorKey;
        SELECT authorstab.FirstName, authorstab.LastName FROM authorstab WHERE idAuthors = @authorKey;
    END IF;

    SET @bookKey = findBookKeyFast('Baxter', 'Stephen', 'Stone Spring', 'Mass Market Paperback');
    IF (@bookKey != 6) THEN
        SELECT @procName, @bookKey;
        SELECT * FROM bookinfo WHERE bookinfo.idBookInfo = @bookKey;
    END IF;

    SET @titleKey = findTitleKey('Star Guard');
    IF (@titleKey != 5) THEN
        SELECT @procName, @titleKey;
        SELECT * FROM title WHERE title.idTitle = @titleKey;
    END IF;

    SET @categoryKey = findCategoryKeyFromStr('Non-Fiction: Electrical Engineering');
    IF (@categoryKey != 5) THEN
        SELECT @procName, @categoryKey;
        SELECT * FROM bookcategories; -- WHERE bookcategories.idBookCategories = @categoryKey;
    END IF;

    SET @formatKey = findFormatKeyFromStr('Mass Market Paperback');
    IF (@formatKey != 4) THEN
        SELECT @procName, @formatKey;
        SELECT * FROM bookformat WHERE bookformat.idFormat = @formatKey;
    END IF;

    SET @seriesKey = findSeriesKey('David', 'Weber', 'Honorverse');
    IF (@seriesKey != 3) THEN
        SELECT @procName, @seriesKey;
        SELECT * FROM series WHERE series.idSeries = @seriesKey;
    END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestInitProcedure` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestInitProcedure`()
    DETERMINISTIC
BEGIN

DECLARE primaryKey INT;

SET @procName = 'zzzUnitTestInitProcedure';

    CALL initBookInventoryTool();
    SELECT COUNT(*) INTO @formatCount FROM pacswlibinvtool.bookformat;
    IF @formatCount != 9 THEN
        SELECT @procName, @formatCount;
        SELECT * FROM bookformat;
    END IF;
    -- the following call to addFormat should result in no changed to the database
    -- since eBook PDF is already in the database.
    CALL addFormat('eBook PDF', primaryKey);
    IF (SELECT COUNT(*) FROM bookformat) != @formatCount THEN
        SELECT @procName, @formatCount, COUNT(*) FROM bookformat;
        SELECT * FROM bookformat;
    END IF;

    SELECT COUNT(*) INTO @categoryCount FROM bookcategories;
    IF @categoryCount != 18 THEN
        SELECT @procName, @categoryCount;
        SELECT * FROM bookcategories;
    END IF;
    -- Should not be added a second time.
    CALL addCategory('Non-Fiction: Electrical Engineering', primaryKey);
    IF (SELECT COUNT(*) FROM bookcategories) != @categoryCount THEN
        SELECT @procName, @categoryCount, COUNT(*) FROM bookcategories;
        SELECT * FROM bookcategories;
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `zzzUnitTestUserUpdates` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `zzzUnitTestUserUpdates`()
    DETERMINISTIC
BEGIN

    DECLARE bookKey INT;
    SET @procName = 'zzzUnitTestUserUpdates';

    SELECT COUNT(*) INTO @forSaleCount FROM forsale WHERE forsale.IsForSale = 1;
    CALL putBookUpForSale('David', 'Weber', 'Honor of the Queen', 'Mass Market Paperback', 10.99, 7.99);
    IF (SELECT COUNT(*) FROM forsale WHERE forsale.IsForSale = 1) != (@forSaleCount + 1) THEN
        SELECT @procName, COUNT(*) FROM forsale;
        SELECT * FROM forsale;
    END IF;
    SELECT COUNT(*) INTO @forSaleCount FROM forsale;
    
    SELECT COUNT(*) INTO @haveReadCount FROM bookcondition WHERE bookcondition.BookHasBeenRead = 1;
    CALL finishedReadingBook('Stephen', 'Baxter', 'Stone Spring', 'Mass Market Paperback');
    CALL finishedReadingBook('Stephen', 'Baxter', 'Bronze Summer', 'Mass Market Paperback');
    IF (SELECT COUNT(*) FROM bookcondition WHERE bookcondition.BookHasBeenRead = 1) != (@haveReadCount + 2) THEN
        SELECT @procName, COUNT(*) FROM bookcondition;
        SELECT * FROM bookcondition;
    END IF;

    CALL bookSold('David', 'Weber', 'Honor of the Queen', 'Mass Market Paperback');
    IF (SELECT COUNT(*) FROM forsale) != (@forSaleCount - 1) THEN
        SELECT @procName, COUNT(*) FROM forsale;
        SELECT * FROM forsale;
    END IF;

    -- Test update buy buying wish listed book.
    Set @buyDate = CURDATE();
    CALL buyBook('Fiction: Science Fiction', 'Norton', 'Andre', 'Star Guard',  'Mass Market Paperback', '1955', '978-0-345-35036-7', 3, 4, 'Harcourt', 0,
        NULL, NULL, NULL, 0, 'Testing 1 2 3', @buyDate, 7.99, 7.99, 'Amazon', bookKey);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-02-15  6:45:02
