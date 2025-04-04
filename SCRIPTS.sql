DROP TABLE `cash_assets`,`expense_category`, `salary_category`, `salary_category_teacher`, `salary_cat_amount`,`class_fees`, `fees_category`, `fees_category_student`, `fees_cat_amount`,`invoice`, `invoice_deleted`,`payment`, `salary`, `ac_ledger`, `transport_fee`, `ac_voucher_types`, `ac_ledger_category`, `ac_ledger_head`, `ac_transaction`, `ac_transaction_entries`, `fee_by_subject_teacher`, `fees_cat_byhistory`, `fees_cat_paidhistory`, `fees_installment`;
DROP VIEW `salarys`;


ALTER TABLE activity_category 
ENGINE=InnoDB;
ALTER TABLE activity_log 
ENGINE=InnoDB;
ALTER TABLE admin 
ENGINE=InnoDB;
ALTER TABLE attendance 
ENGINE=InnoDB;
ALTER TABLE book 
ENGINE=InnoDB;
ALTER TABLE book_issue 
ENGINE=InnoDB;
ALTER TABLE books_category 
ENGINE=InnoDB;
ALTER TABLE cash_assets 
ENGINE=InnoDB;
ALTER TABLE ci_sessions 
ENGINE=InnoDB;
ALTER TABLE class 
ENGINE=InnoDB;
ALTER TABLE class_routine 
ENGINE=InnoDB;
ALTER TABLE document 
ENGINE=InnoDB;
ALTER TABLE dormitory 
ENGINE=InnoDB;
ALTER TABLE exam 
ENGINE=InnoDB;
ALTER TABLE exam_student_comment 
ENGINE=InnoDB;
ALTER TABLE expense_category 
ENGINE=InnoDB;
ALTER TABLE grade 
ENGINE=InnoDB;
ALTER TABLE institute 
ENGINE=InnoDB;
ALTER TABLE `language` 
ENGINE=InnoDB;
ALTER TABLE mark 
ENGINE=InnoDB;
ALTER TABLE material_type 
ENGINE=InnoDB;
ALTER TABLE message 
ENGINE=InnoDB;
ALTER TABLE message_thread 
ENGINE=InnoDB;
ALTER TABLE noticeboard 
ENGINE=InnoDB;
ALTER TABLE parent 
ENGINE=InnoDB;
ALTER TABLE `section` 
ENGINE=InnoDB;
ALTER TABLE `session` 
ENGINE=InnoDB;
ALTER TABLE settings 
ENGINE=InnoDB;
ALTER TABLE sms_que 
ENGINE=InnoDB;
ALTER TABLE staff_type 
ENGINE=InnoDB;
ALTER TABLE student 
ENGINE=InnoDB;
ALTER TABLE subject 
ENGINE=InnoDB;
ALTER TABLE synchro 
ENGINE=InnoDB;
ALTER TABLE tbl_biometric_devices 
ENGINE=InnoDB;
ALTER TABLE tbl_bonus_points 
ENGINE=InnoDB;
ALTER TABLE tbl_holidays 
ENGINE=InnoDB;
ALTER TABLE tbl_mark_subject 
ENGINE=InnoDB;
ALTER TABLE tbllog 
ENGINE=InnoDB;
ALTER TABLE teacher 
ENGINE=InnoDB;
ALTER TABLE time_table_assign 
ENGINE=InnoDB;
ALTER TABLE time_table_slot 
ENGINE=InnoDB;
ALTER TABLE transport 
ENGINE=InnoDB;


CREATE TABLE `category` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(1000) DEFAULT NULL,
  `type_id` int DEFAULT NULL,
  `parent_id` int DEFAULT '0',
  `subparent_id` int DEFAULT '0',
  `status` tinyint(1) DEFAULT '1',
  `field1` varchar(1000) DEFAULT NULL,
  `field2` varchar(1000) DEFAULT NULL,
  `field3` varchar(1000) DEFAULT NULL,
  `field4` varchar(1000) DEFAULT NULL,
  `field5` varchar(1000) DEFAULT NULL,
  `field6` varchar(100) DEFAULT NULL,
  `description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `category_assign` (
  `id` int NOT NULL AUTO_INCREMENT,
  `main_id` int NOT NULL DEFAULT '0',
  `assign_id` int NOT NULL DEFAULT '0',
  `val` varchar(1000) NOT NULL DEFAULT '',
  `type_id` int NOT NULL COMMENT 'category_assign_type',
  `sequence` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `transactions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `payment` enum('paid','receive','journal') DEFAULT NULL,
  `type_id` int NOT NULL,
  `amount` double(15,2) DEFAULT NULL,
  `date` date NOT NULL,
  `remarks` text NOT NULL,
  `attachment` varchar(500) NOT NULL,
  `invoice_id` int DEFAULT '0',
  `user_id` int NOT NULL,
  `refernce` varchar(255) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `transactions_type_FK` (`type_id`),
  KEY `transactions_admin_FK` (`user_id`),
  KEY `transactions_invoice_FK` (`invoice_id`),
  CONSTRAINT `transactions_admin_FK` FOREIGN KEY (`user_id`) REFERENCES `admin` (`admin_id`),
  CONSTRAINT `transactions_invoice_FK` FOREIGN KEY (`invoice_id`) REFERENCES `invoice` (`id`),
  CONSTRAINT `transactions_type_FK` FOREIGN KEY (`type_id`) REFERENCES `category` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `transaction_entries` (
  `id` int NOT NULL AUTO_INCREMENT,
  `transaction_id` int DEFAULT NULL,
  `ledger_id` int DEFAULT NULL,
  `debit` decimal(20,2) DEFAULT '0.00',
  `credit` decimal(20,2) DEFAULT '0.00',
  PRIMARY KEY (`id`),
  KEY `transaction_entries_transactions_FK` (`transaction_id`),
  KEY `transaction_entries_ledger_FK` (`ledger_id`),
  CONSTRAINT `transaction_entries_ledger_FK` FOREIGN KEY (`ledger_id`) REFERENCES `ledger` (`id`),
  CONSTRAINT `transaction_entries_transactions_FK` FOREIGN KEY (`transaction_id`) REFERENCES `transactions` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


CREATE TABLE `ledger` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `phone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `attachment_id` int DEFAULT NULL,
  `parent_id` int DEFAULT NULL,
  `level_id` int DEFAULT '0',
  `opening_debit` double(15,2) NOT NULL DEFAULT '0.00',
  `city_id` int DEFAULT NULL,
  `country_id` int DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `status` tinyint NOT NULL DEFAULT '1',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `user_id` int DEFAULT '0',
  `opening_credit` double(15,2) DEFAULT '0.00',
  `type` enum('parent','vendor','expense','income') COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `ledger_ledger_FK` (`parent_id`),
  KEY `ledger_admin_FK` (`user_id`),
  CONSTRAINT `ledger_admin_FK` FOREIGN KEY (`user_id`) REFERENCES `admin` (`admin_id`),
  CONSTRAINT `ledger_ledger_FK` FOREIGN KEY (`parent_id`) REFERENCES `ledger` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


CREATE TABLE `invoice` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `staff_id` int NOT NULL,
  `class_id` int DEFAULT NULL,
  `invoice_type` int DEFAULT NULL COMMENT '''fees'',''fee_concession'',''fee_class'',''salary_employee'',''salary'',''sale'',''purchase'',''sale_return'',''purchase_return''',
  `month` date NOT NULL,
  `from_date` date NOT NULL,
  `to_date` date NOT NULL,
  `addition` double(10,2) NOT NULL DEFAULT '0.00',
  `cutoff` double(10,2) NOT NULL DEFAULT '0.00',
  `amount` double(10,2) NOT NULL DEFAULT '0.00',
  `pay_date` date DEFAULT NULL,
  `payment_method` tinyint(1) DEFAULT NULL COMMENT '1:cash,2:bank',
  `note` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `status` int NOT NULL DEFAULT '0' COMMENT '0 Unpaid, 1 Paid',
  `created_by` int DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `invoice_student_FK` (`student_id`),
  KEY `invoice_teacher_FK` (`staff_id`),
  KEY `invoice_class_FK` (`class_id`),
  CONSTRAINT `invoice_class_FK` FOREIGN KEY (`class_id`) REFERENCES `class` (`class_id`),
  CONSTRAINT `invoice_student_FK` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`),
  CONSTRAINT `invoice_teacher_FK` FOREIGN KEY (`staff_id`) REFERENCES `teacher` (`teacher_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


CREATE TABLE `invoice_entries` (
  `id` int NOT NULL AUTO_INCREMENT,
  `invoice_id` int NOT NULL,
  `amount` double(10,2) NOT NULL,
  `type` enum('add','minus') DEFAULT 'add' COMMENT 'needed to define student concession,fees generate, employee salary setting and generate',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;