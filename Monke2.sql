-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: boardinghousedb
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `boarders`
--

DROP TABLE IF EXISTS `boarders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `boarders` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int DEFAULT NULL,
  `FullName` varchar(100) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `RoomId` int DEFAULT NULL,
  `IsActive` bit(1) DEFAULT b'1',
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `BoardingHouseId` int DEFAULT NULL,
  `ProfilePicturePath` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `RoomId` (`RoomId`),
  KEY `BoardingHouseId` (`BoardingHouseId`),
  CONSTRAINT `boarders_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`),
  CONSTRAINT `boarders_ibfk_2` FOREIGN KEY (`RoomId`) REFERENCES `rooms` (`Id`),
  CONSTRAINT `boarders_ibfk_3` FOREIGN KEY (`BoardingHouseId`) REFERENCES `boardinghouses` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `boarders`
--

LOCK TABLES `boarders` WRITE;
/*!40000 ALTER TABLE `boarders` DISABLE KEYS */;
INSERT INTO `boarders` VALUES (1,3,'Rodz Cachero','Malalag','12345678901',1,_binary '\0','2026-01-28 17:13:51',1,NULL),(2,4,'Keith Onel Fuentes','Barayong, Magsaysay','12345678902',7,_binary '','2026-01-28 17:19:19',4,'ProfilePictures/c25b52a8-d7a5-4293-bcfc-05a4745b9a19.jpg'),(3,6,'Chrollo','Padada','12345678901',2,_binary '','2026-02-01 22:04:27',1,NULL),(4,NULL,'rodz','Malalag','12345678901',5,_binary '','2026-02-02 14:07:04',1,'ProfilePictures/21404ce6-dcec-4e28-ba15-63d3b92a0638.jpg'),(5,7,'Keith Onel Fuentes','Barayong, Magsaysay','1234567891-',1,_binary '\0','2026-02-04 22:19:04',NULL,'ProfilePictures/d1276d51-909a-4bff-8be8-37d4857040bf.jpg'),(6,11,'maggie','magsaysay','123456789010',8,_binary '','2026-02-09 14:26:16',6,'ProfilePictures/126f4fa6-3c71-413c-9c18-6e12393df8b7.jpg'),(7,12,'klyde','123','12345678910',8,_binary '','2026-02-09 14:28:36',6,'ProfilePictures/9f46879e-c67c-43f1-bf6c-02191cfcd1ff.jpg');
/*!40000 ALTER TABLE `boarders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `boardinghouses`
--

DROP TABLE IF EXISTS `boardinghouses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `boardinghouses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OwnerId` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Description` text,
  `Rules` text,
  `Amenities` text,
  `IsActive` bit(1) DEFAULT b'1',
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `ImagePath1` varchar(500) DEFAULT NULL,
  `ImagePath2` varchar(500) DEFAULT NULL,
  `ImagePath3` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `OwnerId` (`OwnerId`),
  CONSTRAINT `boardinghouses_ibfk_1` FOREIGN KEY (`OwnerId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `boardinghouses`
--

LOCK TABLES `boardinghouses` WRITE;
/*!40000 ALTER TABLE `boardinghouses` DISABLE KEYS */;
INSERT INTO `boardinghouses` VALUES (1,1,'My Main Boarding House','Default Address','Main Property',NULL,NULL,_binary '','2026-02-04 21:38:29',NULL,NULL,NULL),(2,2,'Alisoso BH','HV Tablizo Matti Digos City','warm, cozy comfy','no boys and girl on a same room, curfew hours 9:00 pm','safe. clean, and fast internet',_binary '\0','2026-02-04 22:17:15',NULL,NULL,NULL),(3,10,'Alisoso BH','HV Tablizo Street','comfy, cozy affordable.','curfew 10 pm','fast internet, cozy, 4 per person rooms for 1k, and 2 per person room for 4k',_binary '\0','2026-02-05 01:28:41',NULL,NULL,NULL),(4,10,'Alisoso BH','HV Tablizo Street','comfy, cozy affordable.','curfew 10 pm','fast internet, cozy, 4 per person rooms for 1k, and 2 per person room for 4k',_binary '','2026-02-05 01:28:58','BoardingHousePictures/6ce6957a-17cb-46a4-9c5f-739e7ec406a1.jpg','BoardingHousePictures/7e41a97a-1e7c-485e-9714-d7b5ee04d778.jpg','BoardingHousePictures/f79f6ca5-7fe1-4798-af8a-0a47449bcf8d.jpg'),(5,10,'Alisoso BH','HV Tablizo Street','comfy, cozy affordable.','curfew 10 pm','fast internet, cozy, 4 per person rooms for 1k, and 2 per person room for 4k',_binary '\0','2026-02-05 20:49:24',NULL,NULL,NULL),(6,2,'Aj BH','Matti Digos City','Cozy, and affordable','No rules','High speed wifi, Owning like your own house, comfortable',_binary '','2026-02-05 21:57:30',NULL,NULL,NULL),(7,10,'My Main Boarding House','Default Address','Main Property','','',_binary '\0','2026-02-09 14:16:11',NULL,NULL,NULL),(8,1,'Alisoso BH','HV Tablizo Street','comfy, cozy affordable.','curfew 10 pm','fast internet, cozy, 4 per person rooms for 1k, and 2 per person room for 4k',_binary '\0','2026-02-11 22:18:38',NULL,NULL,NULL);
/*!40000 ALTER TABLE `boardinghouses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expenses`
--

DROP TABLE IF EXISTS `expenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expenses` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `ExpenseDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `Category` varchar(50) DEFAULT 'General',
  `BoardingHouseId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `BoardingHouseId` (`BoardingHouseId`),
  CONSTRAINT `expenses_ibfk_1` FOREIGN KEY (`BoardingHouseId`) REFERENCES `boardinghouses` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expenses`
--

LOCK TABLES `expenses` WRITE;
/*!40000 ALTER TABLE `expenses` DISABLE KEYS */;
INSERT INTO `expenses` VALUES (1,'Payment nimo dae','paying date',8000.00,'2026-02-02 14:09:35','Other',1);
/*!40000 ALTER TABLE `expenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payments`
--

DROP TABLE IF EXISTS `payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `BoarderId` int NOT NULL,
  `Amount` decimal(10,2) NOT NULL,
  `PaymentDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `MonthPaid` varchar(20) NOT NULL,
  `YearPaid` int NOT NULL,
  `Status` varchar(20) DEFAULT 'Pending',
  `Notes` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `BoarderId` (`BoarderId`),
  CONSTRAINT `payments_ibfk_1` FOREIGN KEY (`BoarderId`) REFERENCES `boarders` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payments`
--

LOCK TABLES `payments` WRITE;
/*!40000 ALTER TABLE `payments` DISABLE KEYS */;
INSERT INTO `payments` VALUES (1,2,2000.00,'2026-01-30 00:37:11','January',2026,'Pending','bayaran ka'),(2,1,100000.00,'2026-01-30 10:23:46','January',2026,'Paid','datoa nimo dol oi I love you'),(3,2,1000.00,'2026-02-11 22:11:26','February',2026,'Pending','bayad choi');
/*!40000 ALTER TABLE `payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rooms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoomNumber` varchar(20) NOT NULL,
  `Capacity` int NOT NULL,
  `MonthlyRate` decimal(10,2) NOT NULL,
  `IsActive` bit(1) DEFAULT b'1',
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `BoardingHouseId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoomNumber` (`RoomNumber`),
  KEY `BoardingHouseId` (`BoardingHouseId`),
  CONSTRAINT `rooms_ibfk_1` FOREIGN KEY (`BoardingHouseId`) REFERENCES `boardinghouses` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms`
--

LOCK TABLES `rooms` WRITE;
/*!40000 ALTER TABLE `rooms` DISABLE KEYS */;
INSERT INTO `rooms` VALUES (1,'101',2,3500.00,_binary '','2026-01-28 16:55:58',1),(2,'102',2,3500.00,_binary '','2026-01-28 16:55:58',1),(3,'201',1,8000.00,_binary '','2026-01-28 16:55:59',1),(4,'103',2,3500.00,_binary '','2026-02-01 22:38:48',1),(5,'202',1,8000.00,_binary '','2026-02-01 22:39:08',1),(7,'401',4,1000.00,_binary '','2026-02-05 22:50:13',4),(8,'501',4,1000.00,_binary '','2026-02-05 23:38:52',6);
/*!40000 ALTER TABLE `rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `Role` varchar(20) NOT NULL,
  `IsActive` bit(1) DEFAULT b'1',
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'superadmin','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','SuperAdmin',_binary '','2026-01-28 16:55:58'),(2,'admin','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','Admin',_binary '','2026-01-28 16:55:58'),(3,'rodz','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-01-28 17:13:51'),(4,'keith','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-01-28 17:19:19'),(5,'john','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4','Admin',_binary '\0','2026-01-30 10:02:37'),(6,'chrollo','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-02-01 22:04:27'),(7,'keith1','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-02-04 22:19:04'),(8,'BlueInn','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Admin',_binary '','2026-02-05 01:25:41'),(9,'kurbada','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Admin',_binary '','2026-02-05 01:26:19'),(10,'alisoso','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Admin',_binary '','2026-02-05 01:27:04'),(11,'maggie','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-02-09 14:26:16'),(12,'klyde','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','Boarder',_binary '','2026-02-09 14:28:36');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-02-16 11:32:31
