TYPE=VIEW
query=select `tnsbay_school_star`.`class`.`class_id` AS `Class_id`,`tnsbay_school_star`.`class`.`name` AS `Class`,`tnsbay_school_star`.`session`.`session_id` AS `SessionID`,`tnsbay_school_star`.`session`.`name` AS `Session`,`tnsbay_school_star`.`subject`.`subject_id` AS `subjectID`,`tnsbay_school_star`.`subject`.`name` AS `subject`,`tnsbay_school_star`.`class`.`time_start` AS `Time_start`,`tnsbay_school_star`.`class`.`time_end` AS `Time_end`,`tnsbay_school_star`.`teacher`.`name` AS `Teacher Name` from ((((`tnsbay_school_star`.`class` join `tnsbay_school_star`.`teacher` on((`tnsbay_school_star`.`teacher`.`teacher_id` = `tnsbay_school_star`.`class`.`teacher_id`))) join `tnsbay_school_star`.`section` on((`tnsbay_school_star`.`section`.`class_id` = `tnsbay_school_star`.`class`.`class_id`))) join `tnsbay_school_star`.`session` on((`tnsbay_school_star`.`session`.`session_id` = `tnsbay_school_star`.`class`.`session_id`))) join `tnsbay_school_star`.`subject` on((`tnsbay_school_star`.`subject`.`section_id` = `tnsbay_school_star`.`section`.`section_id`)))
md5=ab83464bde4807209f6e0b1a93db96c1
updatable=1
algorithm=0
definer_user=tnsbay
definer_host=%
suid=1
with_check_option=0
timestamp=2022-05-11 05:14:27
create-version=1
source=select `class`.`class_id` AS `Class_id`,`class`.`name` AS `Class`,`session`.`session_id` AS `SessionID`,`session`.`name` AS `Session`,`subject`.`subject_id` AS `subjectID`,`subject`.`name` AS `subject`,`class`.`time_start` AS `Time_start`,`class`.`time_end` AS `Time_end`,`teacher`.`name` AS `Teacher Name` from ((((`class` join `teacher` on((`teacher`.`teacher_id` = `class`.`teacher_id`))) join `section` on((`section`.`class_id` = `class`.`class_id`))) join `session` on((`session`.`session_id` = `class`.`session_id`))) join `subject` on((`subject`.`section_id` = `section`.`section_id`)))
client_cs_name=utf8mb4
connection_cl_name=utf8mb4_general_ci
view_body_utf8=select `tnsbay_school_star`.`class`.`class_id` AS `Class_id`,`tnsbay_school_star`.`class`.`name` AS `Class`,`tnsbay_school_star`.`session`.`session_id` AS `SessionID`,`tnsbay_school_star`.`session`.`name` AS `Session`,`tnsbay_school_star`.`subject`.`subject_id` AS `subjectID`,`tnsbay_school_star`.`subject`.`name` AS `subject`,`tnsbay_school_star`.`class`.`time_start` AS `Time_start`,`tnsbay_school_star`.`class`.`time_end` AS `Time_end`,`tnsbay_school_star`.`teacher`.`name` AS `Teacher Name` from ((((`tnsbay_school_star`.`class` join `tnsbay_school_star`.`teacher` on((`tnsbay_school_star`.`teacher`.`teacher_id` = `tnsbay_school_star`.`class`.`teacher_id`))) join `tnsbay_school_star`.`section` on((`tnsbay_school_star`.`section`.`class_id` = `tnsbay_school_star`.`class`.`class_id`))) join `tnsbay_school_star`.`session` on((`tnsbay_school_star`.`session`.`session_id` = `tnsbay_school_star`.`class`.`session_id`))) join `tnsbay_school_star`.`subject` on((`tnsbay_school_star`.`subject`.`section_id` = `tnsbay_school_star`.`section`.`section_id`)))
