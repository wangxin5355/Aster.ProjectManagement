DROP DATABASE IF EXISTS aster.projectmanagement;
CREATE DATABASE IF NOT EXISTS aster.projectmanagement DEFAULT CHARSET utf8 COLLATE utf8_bin;

use aster.projectmanagement;

# 员工表
DROP TABLE IF EXISTS t_employee;
CREATE TABLE IF NOT EXISTS t_employee (
  `id` INT NOT NULL AUTO_INCREMENT COMMENT '员工id',
  `name` VARCHAR(45) NULL COMMENT '身份证上的名字',
  `idCardNo` VARCHAR(100) NULL COMMENT '身份证号码',
  `addr` VARCHAR(255) NULL COMMENT '身份证上地址',
  `liveAddr` VARCHAR(155) NULL COMMENT '居住地址',
  `phone` INT NULL COMMENT '手机号码',
  `defaultPrice` DOUBLE NULL COMMENT '默认时薪',
  `creditLevel` INT NULL COMMENT '信用等级',
  `diligentLevel` INT NULL COMMENT '勤奋等级',
  `idCardPictureUp` BLOB NULL COMMENT '身份证正面',
  `idCardPictureDown` BLOB NULL COMMENT '身份证反面',
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COLLATE = utf8_bin
COMMENT = '员工表';

#员工银行表卡
CREATE TABLE t_employeebanks (
  `id` INT NOT NULL AUTO_INCREMENT,
  `employeeId` INT NULL COMMENT '员工id',
  `bankCardId` VARCHAR(100) NULL COMMENT '银行卡号',
  `bankId` INT  NULL COMMENT '银行id',
  `pwd` VARCHAR(45) NULL COMMENT '银行卡密码',
  PRIMARY KEY (`id`))
COMMENT = '员工银行卡信息';

#银行卡列表
CREATE TABLE t_banks (
  `id` INT NOT NULL AUTO_INCREMENT,
  `bankName` VARCHAR(45) NULL COMMENT '银行名称',
  `bankAddr` VARCHAR(45) NULL COMMENT '开户地址',
  PRIMARY KEY (`id`))
COMMENT = '银行卡列表';


