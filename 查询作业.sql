USE StudentSystem
GO
--查询" 01 "课程比" 02 "课程成绩高的学生的信息及课程分数
SELECT * FROM Student RIGHT JOIN ( SELECT t1.Sid, class1, class2 FROM
 (SELECT Sid, Score AS class1 FROM SC WHERE SC.Cid = '1')AS t1, 
(SELECT Sid, Score AS class2 FROM SC WHERE SC.Cid = '2')AS t2 
WHERE t1.Sid = t2.Sid AND t1.class1 > t2.class2 )r ON Student.Sid = r.Sid
--查询同时存在" 01 "课程和" 02 "课程的情况
SELECT * FROM (SELECT * FROM SC WHERE SC.Cid = '1') AS t1, 
(SELECT * FROM SC WHERE SC.Cid = '2') AS t2 WHERE t1.Sid = t2.Sid
--查询存在" 01 "课程但可能不存在" 02 "课程的情况(不存在时显示为 null )
SELECT * FROM (SELECT * FROM SC WHERE SC.Cid = '1') AS t1 
LEFT JOIN (SELECT * FROM SC WHERE SC.Cid = '2') AS t2 ON t1.Sid = t2.Sid
--查询不存在" 01 "课程但存在" 02 "课程的情况
SELECT * FROM SC WHERE SC.Sid NOT IN ( SELECT Sid FROM SC WHERE SC.Cid = '1' ) AND SC.Cid= '2'
--查询平均成绩大于等于 60 分的同学的学生编号和学生姓名和平均成绩
SELECT Student.Sid AS '学生编号',Sname AS '学生姓名',SS AS '平均成绩' FROM Student,
( SELECT Sid, AVG(Score) AS SS FROM SC GROUP BY Sid HAVING AVG(Score)> 60 )r WHERE Student.Sid = r.Sid
--查询在 SC 表存在成绩的学生信息
SELECT DISTINCT Student. * FROM Student,SC WHERE Student.Sid=SC.Sid 
--查询所有同学的学生编号、学生姓名、选课总数、所有课程的总成绩(没成绩的显示为 null )
SELECT s.Sid AS '学生编号', s.Sname AS '学生姓名',r.coursenumber AS '选课总数',r.scoresum AS '所有课程的总成绩' FROM ( (SELECT Student.Sid,Student.Sname FROM Student )s LEFT JOIN (SELECT SC.Sid, SUM(SC.Score) AS scoresum, COUNT(SC.Cid) AS coursenumber FROM SC GROUP BY SC.Sid )r ON s.Sid = r.Sid )
--查有成绩的学生信息
SELECT * FROM Student WHERE Student.Sid IN (SELECT SC.Sid FROM SC)
--查询「李」姓老师的数量
SELECT COUNT(*) FROM Teacher WHERE Tname LIKE '李%'
--查询学过「张三」老师授课的同学的信息
SELECT Student.* FROM Student,Teacher,Course,SC WHERE Student.Sid = SC.Sid AND Course.Cid=SC.Cid AND Course.Tid = Teacher.Tid AND Tname = '张三'
--查询没有学全所有课程的同学的信息
SELECT * FROM Student WHERE Student.Sid NOT IN (SELECT SC.Sid FROM SC GROUP BY SC.Sid HAVING COUNT(SC.Cid)= (SELECT COUNT(Cid) FROM Course))
--查询至少有一门课与学号为" 01 "的同学所学相同的同学的信息
SELECT * FROM Student WHERE Student.Sid IN (SELECT SC.Sid FROM SC WHERE SC.Cid IN(SELECT SC.Cid FROM SC WHERE SC.Sid = '1'))
--查询没学过"张三"老师讲授的任一门课程的学生姓名
SELECT * FROM Student WHERE Student.Sid NOT IN(SELECT SC.Sid FROM SC,Course,Teacher WHERE SC.Cid = Course.Cid AND Course.Tid = Teacher.Tid AND Teacher.Tname= '张三')
--检索" 01 "课程分数小于 60，按分数降序排列的学生信息
SELECT Student.*, SC.Score FROM Student, SC WHERE Student.Sid = SC.Sid AND SC.Score < '60' AND Cid = '1' ORDER BY SC.Score DESC
--按平均成绩从高到低显示所有学生的所有课程的成绩以及平均成绩
SELECT * FROM SC LEFT JOIN (SELECT Sid,AVG(Score) AS AVSCOLE FROM SC GROUP BY Sid )r ON SC.Sid = r.Sid ORDER BY AVSCOLE DESC
--查询各科成绩最高分、最低分和平均分
SELECT SC.Cid , MAX(SC.Score) AS 最高分, MIN(SC.Score) AS 最低分, AVG(SC.Score) AS 平均分, COUNT(*) AS 选修人数, SUM(CASE WHEN SC.Score>=60 THEN 1 ELSE 0 END )/COUNT(*)AS 及格率, 
SUM(CASE WHEN SC.Score>=70 AND SC.Score<80 THEN 1 ELSE 0 END )/COUNT(*)AS 中等率, 
SUM(CASE WHEN SC.Score>=80 AND SC.Score<90 THEN 1 ELSE 0 END )/COUNT(*)AS 优良率, 
SUM(CASE WHEN SC.Score>=90 THEN 1 ELSE 0 END )/COUNT(*)AS 优秀率 FROM SC GROUP BY SC.Cid ORDER BY COUNT(*)DESC, SC.Cid ASC 
--查询学生的总成绩，并进行排名，总分重复时保留名次空缺
DECLARE @id int
SET @id=(SELECT SId FROM Student WHERE SId=1)
WHILE(@id<19)
  BEGIN
    SELECT SC.SId,Sname,AVG(score) as '平均成绩' FROM SC INNER JOIN Student ON SC.SId=Student.SId WHERE @id=+1  GROUP BY SC.SId,Sname   ORDER BY AVG(score) DESC
    BREAK
  END
--查询 1990 年出生的学生名单
SELECT * FROM Student WHERE YEAR(Student.Sage)='1990'
--查询同名同性学生名单，并统计同名人数
SELECT * FROM Student WHERE Sname IN ( SELECT Sname FROM Student GROUP BY Sname HAVING COUNT(*)>1 )
SELECT Sname, COUNT(*) FROM Student GROUP BY Sname HAVING COUNT(*)>1
--查询名字中含有「风」字的学生信息
SELECT * FROM Student WHERE Student.Sname LIKE '%风%'
--查询男生、女生人数
SELECT Ssex, COUNT(*) FROM Student GROUP BY Ssex
--查询出只选修两门课程的学生学号和姓名
SELECT Student.Sid, Student.Sname FROM Student WHERE Student.Sid IN 
(SELECT SC.Sid FROM SC GROUP BY SC.Sid HAVING COUNT(SC.Cid)=2)
--查询每门课程被选修的学生数
SELECT Cid, COUNT(Sid) FROM SC GROUP BY Cid
--查询各科成绩前三名的记录
SELECT * FROM SC WHERE (SELECT COUNT(*) FROM SC AS a WHERE SC.Cid = a.Cid AND SC.Score<a.Score)< 3 ORDER BY Cid ASC, SC.Score DESC
--按各科成绩进行排序，并显示排名， Score 重复时保留名次空缺
SELECT a.Cid, a.Sid, a.Score, COUNT(b.Score)+1 AS RANK FROM SC AS a LEFT JOIN SC AS b ON a.Score<b.Score AND a.Cid = b.Cid GROUP BY a.Cid, a.Sid,a.Score ORDER BY a.Cid, RANK ASC
--查询和" 01 "号的同学学习的课程 完全相同的其他同学的信息
SELECT Student.* FROM Student WHERE Sid in (SELECT distinct SC.Sid FROM SC WHERE Sid <> '01' AND SC.Cid in (SELECT distinct Cid FROM SC WHERE Sid = '01') GROUP BY SC.Sid HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE Sid='01'))SELECT Student.* FROM Student WHERE Sid in
(SELECT distinct SC.Sid FROM SC WHERE Sid <> '01' AND SC.Cid in (SELECT distinct Cid FROM SC WHERE Sid = '01') GROUP BY SC.Sid HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE Sid='01'))
--查询两门及其以上不及格课程的同学的学号，姓名及其平均成绩
SELECT Student.Sid , Student.Sname , CAST(AVG(Score) AS decimal(18,2)) AVG_Score FROM Student , SC WHERE Student.SID = SC.SID AND Student.Sid IN (SELECT Sid FROM SC WHERE Score < 60 GROUP BY Sid HAVING COUNT(1) >= 2) GROUP BY Student.Sid , Student.Sname
--按各科成绩进行排序，并显示排名， Score 重复时合并名次
SELECT t.* , px = (SELECT COUNT(distinct Score) FROM SC WHERE Cid = t.Cid AND Score >= t.Score) FROM SC t order BY t.Cid , px
--查询学生的总成绩，并进行排名，总分重复时不保留名次空缺
SELECT t.* , px = DENSE_RANK() OVER(ORDER BY 总成绩 DESC) FROM (SELECT m.Sid 学生编号 ,m.Sname 学生姓名 ,isnull(SUM(Score),0) 总成绩 FROM Student m LEFT JOIN SC n ON m.Sid = n.Sid GROUP BY m.Sid , m.Sname) t ORDER BY px
--统计各科成绩各分数段人数：课程编号，课程名称，[100-85]，[85-70]，[70-60]，[60-0] 及所占百分比
SELECT Course.Cname, Course.Cid,
SUM(Case WHEN SC.Score<=100 AND SC.Score>85 THEN 1 ELSE 0 END) AS '[100-85]',
SUM(Case WHEN SC.Score<=85 AND SC.Score>70 THEN 1 ELSE 0 END) AS '[85-70]',
SUM(Case WHEN SC.Score<=70 AND SC.Score>60 THEN 1 ELSE 0 END) AS '[70-60]',
SUM(Case WHEN SC.Score<=60 AND SC.Score>0 THEN 1 ELSE 0 END) AS '[60-0]'
FROM SC LEFT JOIN Course
ON SC.Cid = Course.Cid
GROUP BY SC.Cid,Course.Cname,Course.Cid
--查询每门课程的平均成绩，结果按平均成绩降序排列，平均成绩相同时，按课程编号升序排列
SELECT m.CiD , m.Cname , CAST(AVG(n.Score) AS DECIMAL(18,2)) AVG_Score FROM Course m, SC n WHERE m.CiD = n.CiD GROUP BY m.CiD , m.Cname ORDER BY AVG_Score DESC, m.CiD ASC
--查询平均成绩大于等于 85 的所有学生的学号、姓名和平均成绩
SELECT a.SiD , a.Sname , CAST(AVG(b.score) AS DECIMAL(18,2)) AVG_Score FROM Student a , SC b WHERE a.SiD = b.SiD GROUP BY a.SiD , a.Sname HAVING CAST(AVG(b.score) AS DECIMAL(18,2)) >= 85 ORDER BY a.SiD
--查询课程名称为「数学」，且分数低于 60 的学生姓名和分数
SELECT Sname , Score FROM Student , SC , Course WHERE SC.SiD = Student.SiD AND SC.CiD = Course.CiD AND Course.Cname = N'数学' AND Score < 60
--查询所有学生的课程及分数情况（存在学生没成绩，没选课的情况）
SELECT Student.* , Course.Cname , SC.CiD , SC.Score FROM Student, SC , Course WHERE Student.SiD = SC.SiD AND SC.CiD = Course.CiD ORDER BY Student.SiD , SC.CiD
--查询任何一门课程成绩在 70 分以上的姓名、课程名称和分数
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score >= 70 ORDER BY Student.SID , SC.CID
--查询不及格的课程
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score < 60 ORDER BY Student.SID , SC.CID
--查询课程编号为 01 且课程成绩在 80 分以上的学生的学号和姓名
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.CID = '1' and SC.score >= 80 ORDER BY Student.SID , SC.CID
--求每门课程的学生人数
SELECT Course.CID , Course.Cname , count(*) 学生人数 FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname ORDER BY Course.CID , Course.Cname
--成绩不重复，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩
SELECT top 1 Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course , Teacher WHERE Student.SID = SC.SID and SC.CID = Course.CID and Course.TID = Teacher.TID and Teacher.Tname = N'张三' ORDER BY SC.score DESC
--成绩有重复的情况下，查询选修「张三」老师所授课程的学生中，成绩最高的学生信息及其成绩
SELECT student.*, sc.score, sc.cid FROM student, teacher, course,sc 
WHERE teacher.tid = course.tid
AND sc.sid = student.sid
AND sc.cid = course.cid
AND teacher.tname = '张三'
AND sc.score = (
    SELECT Max(sc.score) 
    FROM sc,student, teacher, course
    WHERE teacher.tid = course.tid
    AND sc.sid = student.sid
    AND sc.cid = course.cid
    AND teacher.tname = '张三'
)
--查询不同课程成绩相同的学生的学生编号、课程编号、学生成绩
SELECT m.* FROM SC m ,(SELECT CID , score FROM SC GROUP BY CID , score having count(1) > 1) n WHERE m.CID= n.CID and m.score = n.score ORDER BY m.CID , m.score , m.SID
--查询每门功成绩最好的前两名
SELECT t.* FROM sc t WHERE score in (SELECT top 2 score FROM sc WHERE CID = T.CID ORDER BY score DESC) ORDER BY t.CID , t.score DESC
--统计每门课程的学生选修人数（超过 5 人的课程才统计）。
SELECT Course.CID , Course.Cname , count(*) 学生人数 FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname having count(*) >= 5 ORDER BY 学生人数 DESC , Course.CID
--检索至少选修两门课程的学生学号
SELECT student.SID , student.Sname FROM student , SC WHERE student.SID = SC.SID GROUP BY student.SID , student.Sname having count(1) >= 2 ORDER BY student.SID
--查询选修了全部课程的学生信息
SELECT student.* FROM student WHERE SID in(SELECT SID FROM sc GROUP BY SID HAVING count(1) = (SELECT count(1) FROM course))
--查询各学生的年龄，只按年份来算
SELECT * , DATEDIFF(yy , sage , GETDATE()) 年龄 FROM student
--按照出生日期来算，当前月日 < 出生年月的月日则，年龄减一
SELECT * , case when right(convert(VARCHAR(10),GETDATE(),120),5) < right(convert(VARCHAR(10),sage,120),5) then DATEDIFF(yy , sage , GETDATE()) - 1 else DATEDIFF(yy , sage , GETDATE()) end 年龄 FROM student
--查询本周过生日的学生
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0
--查询下周过生日的学生
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1
--查询本月过生日的学生
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0
--查询下月过生日的学生
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1