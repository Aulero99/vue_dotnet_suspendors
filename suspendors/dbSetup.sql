-- Active: 1690221004246@@52.37.83.226@3306@keepr
DROP Table IF EXISTS vaultKeeps, vaults, keeps;
DROP TABLE IF EXISTS accounts;

CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture',
  coverImg VARCHAR(255) COMMENT 'Cover Image'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(50) NOT NULL,
  description TEXT,
  img TEXT NOT NULL,
  views INT NOT NULL DEFAULT 0,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(50) NOT NULL,
  description TEXT,
  img TEXT NOT NULL,
  isPrivate TINYINT NOT NULL DEFAULT 0,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

SELECT 
k.*, 
a.*,
COUNT(v.id) AS Kept
FROM keeps k 
JOIN accounts a ON a.id = k.CreatorId
LEFT JOIN vaultKeeps v ON v.keepId = k.id
WHERE k.id = LAST_INSERT_ID();


SELECT 
k.*, 
COUNT(v.id) AS Kept,
a.*
FROM keeps k 
LEFT JOIN vaultKeeps v ON v.keepId = k.id
LEFT JOIN accounts a ON k.CreatorId = a.id
WHERE k.id = LAST_INSERT_ID()
GROUP BY (k.id);