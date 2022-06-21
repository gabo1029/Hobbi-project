-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql.omega:3306
-- Létrehozás ideje: 2022. Jún 21. 17:38
-- Kiszolgáló verziója: 5.7.38-log
-- PHP verzió: 5.6.40

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `kanocbeton`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `afa`
--

CREATE TABLE `afa` (
  `id` int(11) NOT NULL,
  `afaMerteke` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `afa`
--

INSERT INTO `afa` (`id`, `afaMerteke`) VALUES
(1, '0%'),
(2, '15%'),
(3, '27%');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `BejovoSzamla`
--

CREATE TABLE `BejovoSzamla` (
  `id` int(11) NOT NULL,
  `kiadasTipusId` int(11) NOT NULL,
  `szamlaSorszam` varchar(50) NOT NULL,
  `cegNeve` varchar(150) NOT NULL,
  `teljesitesDatuma` varchar(20) NOT NULL,
  `szamlaKelte` varchar(20) NOT NULL,
  `szamlaLejarata` varchar(20) NOT NULL,
  `fizetveDatum` varchar(20) NOT NULL,
  `fizetveID` int(11) NOT NULL,
  `fizetesiFormaId` int(11) NOT NULL,
  `megjegyzes` varchar(200) NOT NULL,
  `bruttoOsszeg` int(11) NOT NULL,
  `nettoOsszeg` int(11) NOT NULL,
  `afa` int(11) NOT NULL,
  `munka` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `BejovoSzamla`
--

INSERT INTO `BejovoSzamla` (`id`, `kiadasTipusId`, `szamlaSorszam`, `cegNeve`, `teljesitesDatuma`, `szamlaKelte`, `szamlaLejarata`, `fizetveDatum`, `fizetveID`, `fizetesiFormaId`, `megjegyzes`, `bruttoOsszeg`, `nettoOsszeg`, `afa`, `munka`) VALUES
(125, 3, '131312', 'adas', '2021. 11. 10.', '2021. 11. 03.', '2021. 11. 12.', '2021. 11. 11.', 3, 2, 'sdasdasd', 121212, 88485, 32727, 'vadasda'),
(169, 1, '', '', '', '', '', '', 2, 2, '', 0, 0, 0, ''),
(170, 7, '', '', '', '', '', '', 2, 3, '', 0, 0, 0, ''),
(176, 10, '', '', '', '', '', '', 3, 2, '', 0, 0, 0, ''),
(178, 6, '', '', '', '', '', '', 3, 2, 'Vecséd\r\n', 200000, 170000, 30000, ''),
(179, 3, '', '', '', '', '', '', 2, 3, '', 11, 8, 3, ''),
(180, 2, '', '', '', '', '', '', 3, 2, '', 0, 0, 0, ''),
(188, 3, '12', '', '2022. 01. 21.', '2022. 01. 20.', '2022. 01. 26.', '2022. 01. 18.', 2, 2, '', 10000, 10000, 0, ''),
(189, 4, 'asda', 'adad', '2022. 06. 09.', '2022. 06. 16.', '2022. 06. 16.', '2022. 06. 08.', 2, 2, 'adada', 124500, 90885, 33615, 'ASDA');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `FizetesiForma`
--

CREATE TABLE `FizetesiForma` (
  `id` int(11) NOT NULL,
  `fizetesiFormaNeve` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `FizetesiForma`
--

INSERT INTO `FizetesiForma` (`id`, `fizetesiFormaNeve`) VALUES
(1, 'Átutalás'),
(2, 'Bankkártya'),
(3, 'Készpénz');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `Fizetve`
--

CREATE TABLE `Fizetve` (
  `id` int(11) NOT NULL,
  `fizetveTipus` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `Fizetve`
--

INSERT INTO `Fizetve` (`id`, `fizetveTipus`) VALUES
(1, 'Fizetve'),
(2, 'Részben fizetve'),
(3, 'Nincs fizetve');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `GarancialisVisszatartas`
--

CREATE TABLE `GarancialisVisszatartas` (
  `id` int(11) NOT NULL,
  `GarancialisVisszatartas` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `GarancialisVisszatartas`
--

INSERT INTO `GarancialisVisszatartas` (`id`, `GarancialisVisszatartas`) VALUES
(1, '5%'),
(2, '10%');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `JoteljesitesiGarancia`
--

CREATE TABLE `JoteljesitesiGarancia` (
  `id` int(11) NOT NULL,
  `JoteljesitesiGarancia` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `JoteljesitesiGarancia`
--

INSERT INTO `JoteljesitesiGarancia` (`id`, `JoteljesitesiGarancia`) VALUES
(1, '5%'),
(2, '10%');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `KiadasTipusok`
--

CREATE TABLE `KiadasTipusok` (
  `id` int(11) NOT NULL,
  `kiadasTipus` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `KiadasTipusok`
--

INSERT INTO `KiadasTipusok` (`id`, `kiadasTipus`) VALUES
(1, 'Tagi kölcsön'),
(2, 'Tagi kölcsön f'),
(3, 'Tankolás AUTÓK'),
(4, 'Tankolás GÉPEK(benzin)'),
(5, 'Tankolás'),
(6, 'Ford költségek ZSOLTI'),
(7, 'Ford költségek RICSI'),
(8, 'Ducato költségek RWV-568'),
(9, 'Benzin'),
(10, '16516'),
(11, 'asd'),
(12, '123');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `KimenoSzamalk`
--

CREATE TABLE `KimenoSzamalk` (
  `id` int(11) NOT NULL,
  `munkaSzam` int(11) NOT NULL,
  `szamlaSorszam` varchar(150) NOT NULL,
  `cegNeve` varchar(150) NOT NULL,
  `teljesitesDatuma` varchar(20) NOT NULL,
  `szamlaKelte` varchar(20) NOT NULL,
  `szamlaLejarata` varchar(20) NOT NULL,
  `fizetveDatuma` varchar(20) NOT NULL,
  `fizetveId` int(11) NOT NULL,
  `fizetesiFormaId` int(11) NOT NULL,
  `munkaNev` varchar(150) NOT NULL,
  `bruttoOsszeg` int(11) NOT NULL,
  `nettoOsszeg` int(11) NOT NULL,
  `afa` int(11) NOT NULL,
  `visszatartasOsszege` int(11) NOT NULL,
  `visszatartasLejarata` varchar(20) NOT NULL,
  `orak` int(11) NOT NULL,
  `oradij` int(11) NOT NULL,
  `megjegyzes` varchar(200) NOT NULL,
  `egyeb` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- A tábla adatainak kiíratása `KimenoSzamalk`
--

INSERT INTO `KimenoSzamalk` (`id`, `munkaSzam`, `szamlaSorszam`, `cegNeve`, `teljesitesDatuma`, `szamlaKelte`, `szamlaLejarata`, `fizetveDatuma`, `fizetveId`, `fizetesiFormaId`, `munkaNev`, `bruttoOsszeg`, `nettoOsszeg`, `afa`, `visszatartasOsszege`, `visszatartasLejarata`, `orak`, `oradij`, `megjegyzes`, `egyeb`) VALUES
(1, 1231231, '1231231', 'asd', '2021-08-04', '2021-08-10', '2021-08-18', '2021-08-25', 1, 2, '', 1231, 131, 1231, 0, '2021-08-31', 100, 1400, 'adfsa', 'asff'),
(2, 1231231, 'asdas', 'asdasdasd', '2021-01-05', '2021-07-13', '2021-07-24', '2021-08-25', 3, 3, 'asdasdasdasd', 120000, 100000, 27, 10000, '2021-08-31', 100, 1400, 'asdas', 'asdasd'),
(3, 123213, 'asda', 'sdasd', '2021-01-05', '2021-07-22', '2021-07-08', '2021-08-25', 1, 2, 'üllő', 120000, 100000, 27, 0, '2021-08-31', 100, 123, 'asda', 'adasd'),
(4, 123213, 'asda', 'sdasd', '2021-01-05', '2021-07-22', '2021-07-08', '2021-08-25', 1, 2, 'üllő', 120000, 100000, 27, 0, '2021-08-31', 100, 123, 'asda', 'adasd');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `MegrendeloiSzolgaltatas`
--

CREATE TABLE `MegrendeloiSzolgaltatas` (
  `id` int(11) NOT NULL,
  `MegrendeloiSzolgaltatas` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `afa`
--
ALTER TABLE `afa`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `BejovoSzamla`
--
ALTER TABLE `BejovoSzamla`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kiadasTipusId` (`kiadasTipusId`),
  ADD KEY `fizetveID` (`fizetveID`,`fizetesiFormaId`),
  ADD KEY `fizetesiFormaId` (`fizetesiFormaId`);

--
-- A tábla indexei `FizetesiForma`
--
ALTER TABLE `FizetesiForma`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `Fizetve`
--
ALTER TABLE `Fizetve`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `GarancialisVisszatartas`
--
ALTER TABLE `GarancialisVisszatartas`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `JoteljesitesiGarancia`
--
ALTER TABLE `JoteljesitesiGarancia`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `KiadasTipusok`
--
ALTER TABLE `KiadasTipusok`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `KimenoSzamalk`
--
ALTER TABLE `KimenoSzamalk`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fizetveId` (`fizetveId`,`fizetesiFormaId`),
  ADD KEY `fizetesiFormaId` (`fizetesiFormaId`);

--
-- A tábla indexei `MegrendeloiSzolgaltatas`
--
ALTER TABLE `MegrendeloiSzolgaltatas`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `afa`
--
ALTER TABLE `afa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `BejovoSzamla`
--
ALTER TABLE `BejovoSzamla`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=190;

--
-- AUTO_INCREMENT a táblához `FizetesiForma`
--
ALTER TABLE `FizetesiForma`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `Fizetve`
--
ALTER TABLE `Fizetve`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `JoteljesitesiGarancia`
--
ALTER TABLE `JoteljesitesiGarancia`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `KiadasTipusok`
--
ALTER TABLE `KiadasTipusok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT a táblához `KimenoSzamalk`
--
ALTER TABLE `KimenoSzamalk`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `MegrendeloiSzolgaltatas`
--
ALTER TABLE `MegrendeloiSzolgaltatas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `BejovoSzamla`
--
ALTER TABLE `BejovoSzamla`
  ADD CONSTRAINT `BejovoSzamla_ibfk_1` FOREIGN KEY (`kiadasTipusId`) REFERENCES `KiadasTipusok` (`id`),
  ADD CONSTRAINT `BejovoSzamla_ibfk_2` FOREIGN KEY (`fizetesiFormaId`) REFERENCES `FizetesiForma` (`id`),
  ADD CONSTRAINT `BejovoSzamla_ibfk_3` FOREIGN KEY (`fizetveID`) REFERENCES `Fizetve` (`id`);

--
-- Megkötések a táblához `KimenoSzamalk`
--
ALTER TABLE `KimenoSzamalk`
  ADD CONSTRAINT `KimenoSzamalk_ibfk_1` FOREIGN KEY (`fizetveId`) REFERENCES `Fizetve` (`id`),
  ADD CONSTRAINT `KimenoSzamalk_ibfk_2` FOREIGN KEY (`fizetesiFormaId`) REFERENCES `FizetesiForma` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
