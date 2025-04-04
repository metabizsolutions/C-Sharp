TYPE=VIEW
query=select `tnsbay_school_star`.`class`.`class_id` AS `Class_id`,`tnsbay_school_star`.`class`.`degree_id` AS `degree_id`,`tnsbay_school_star`.`class`.`name` AS `Name`,`tnsbay_school_star`.`class`.`name_digit` AS `name_digit`,`tnsbay_school_star`.`session`.`name` AS `Session`,`tnsbay_school_star`.`class`.`time_start` AS `Time_start`,`tnsbay_school_star`.`class`.`time_end` AS `Time_end`,`tnsbay_school_star`.`teacher`.`name` AS `Teacher Name` from ((`tnsbay_school_star`.`class` left join `tnsbay_school_star`.`teacher` on((`tnsbay_school_star`.`teacher`.`teacher_id` = `tnsbay_school_star`.`class`.`teacher_id`))) join `tnsbay_school_star`.`session` on((`tnsbay_school_star`.`session`.`session_id` = `tnsbay_school_star`.`class`.`session_id`)))
md5=8e7eb8e23507a36739963f317d2451c3
updatable=0
algorithm=0
definer_user=root
definer_host=localhost
suid=1
with_check_option=0
timestamp=2022-05-11 05:14:27
create-version=1
source=select `class`.`class_id` AS `Class_id`,`class`.`degree_id` AS `degree_id`,`class`.`name` AS `Name`,`class`.`name_digit` AS `name_digit`,`session`.`name` AS `Session`,`class`.`time_start` AS `Time_start`,`class`.`time_end` AS `Time_end`,`teacher`.`name` AS `Teacher Name` from ((`class` left join `teacher` on((`teacher`.`teacher_id` = `class`.`teacher_id`))) join `session` on((`session`.`session_id` = `class`.`session_id`)))
client_cs_name=utf8mb4
connection_cl_name=utf8mb4_general_ci
view_body_utf8=select `tnsbay_school_star`.`class`.`class_id` AS `Class_id`,`tnsbay_school_star`.`class`.`degree_id` AS `degree_id`,`tnsbay_school_star`.`class`.`name` AS `Name`,`tnsbay_school_star`.`class`.`name_digit` AS `name_digit`,`tnsbay_school_star`.`session`.`name` AS `Session`,`tnsbay_school_star`.`class`.`time_start` AS `Time_start`,`tnsbay_school_star`.`class`.`time_end` AS `Time_end`,`tnsbay_school_star`.`teacher`.`name` AS `Teacher Name` from ((`tnsbay_school_star`.`class` left join `tnsbay_school_star`.`teacher` on((`tnsbay_school_star`.`teacher`.`teacher_id` = `tnsbay_school_star`.`class`.`teacher_id`))) join `tnsbay_school_star`.`session` on((`tnsbay_school_star`.`session`.`session_id` = `tnsbay_school_star`.`class`.`session_id`)))
