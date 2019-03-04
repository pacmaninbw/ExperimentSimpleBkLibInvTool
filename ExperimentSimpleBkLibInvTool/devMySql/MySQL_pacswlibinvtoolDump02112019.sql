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
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `authorstab`
--

LOCK TABLES `authorstab` WRITE;
/*!40000 ALTER TABLE `authorstab` DISABLE KEYS */;
INSERT INTO `authorstab` VALUES (1,'Heinlein','Robert','Anson','1907','1988'),(2,'Asimov','Isaac',NULL,'1920','1992'),(3,'Clarke','Arthur','Charles','1917','2008'),(4,'Le Guin','Ursula','Kroeber','1929','2018'),(5,'Bradbury','Ray','Douglas ','1920','2012'),(6,'Dick','Philip','Kindred','1928','1982'),(7,'Wells','Herbert','George','1866','1946'),(8,'Silverberg','Robert',NULL,'1935',NULL),(9,'Zimmer Bradley','Marion','Eleanor','1930','1999'),(10,'Norton','Andre','Alice','1912','2005'),(11,'Drake','David',NULL,'1945',NULL),(12,'Weber','David','Mark','1952',NULL),(13,'Baxter','Stephen',NULL,'1957',NULL),(15,'Brin','David','Glen','1950',NULL),(16,'Oneal','Bill',NULL,'1977',NULL),(17,'Herbert','Frank','Patrick','1920','1986'),(18,'Herbert','Brian','Patrick','1967',NULL),(19,'Anthony','Piers',NULL,'1934',NULL),(20,'Stevenson','Robert','Louis','1850','1894'),(21,'Wright','Frank','Lloyd','1867','1959'),(22,'Bradley','Omar','Nelson','1893','1981'),(23,'Chernick','Goldie','Pinch','1925','2008'),(24,'Alcot','Louisa','May','1832','1888'),(25,'Braun','Wernher','Von','1912','1977'),(26,'Chernick','Isadore','Ike','1923','1999'),(27,'Pease','Howard','A','1894','1974');
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-02-11  7:42:40
