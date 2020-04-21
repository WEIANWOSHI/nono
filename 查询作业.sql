USE StudentSystem
GO
--��ѯ" 01 "�γ̱�" 02 "�γ̳ɼ��ߵ�ѧ������Ϣ���γ̷���
SELECT * FROM Student RIGHT JOIN ( SELECT t1.Sid, class1, class2 FROM
 (SELECT Sid, Score AS class1 FROM SC WHERE SC.Cid = '1')AS t1, 
(SELECT Sid, Score AS class2 FROM SC WHERE SC.Cid = '2')AS t2 
WHERE t1.Sid = t2.Sid AND t1.class1 > t2.class2 )r ON Student.Sid = r.Sid
--��ѯͬʱ����" 01 "�γ̺�" 02 "�γ̵����
SELECT * FROM (SELECT * FROM SC WHERE SC.Cid = '1') AS t1, 
(SELECT * FROM SC WHERE SC.Cid = '2') AS t2 WHERE t1.Sid = t2.Sid
--��ѯ����" 01 "�γ̵����ܲ�����" 02 "�γ̵����(������ʱ��ʾΪ null )
SELECT * FROM (SELECT * FROM SC WHERE SC.Cid = '1') AS t1 
LEFT JOIN (SELECT * FROM SC WHERE SC.Cid = '2') AS t2 ON t1.Sid = t2.Sid
--��ѯ������" 01 "�γ̵�����" 02 "�γ̵����
SELECT * FROM SC WHERE SC.Sid NOT IN ( SELECT Sid FROM SC WHERE SC.Cid = '1' ) AND SC.Cid= '2'
--��ѯƽ���ɼ����ڵ��� 60 �ֵ�ͬѧ��ѧ����ź�ѧ��������ƽ���ɼ�
SELECT Student.Sid AS 'ѧ�����',Sname AS 'ѧ������',SS AS 'ƽ���ɼ�' FROM Student,
( SELECT Sid, AVG(Score) AS SS FROM SC GROUP BY Sid HAVING AVG(Score)> 60 )r WHERE Student.Sid = r.Sid
--��ѯ�� SC ����ڳɼ���ѧ����Ϣ
SELECT DISTINCT Student. * FROM Student,SC WHERE Student.Sid=SC.Sid 
--��ѯ����ͬѧ��ѧ����š�ѧ��������ѡ�����������пγ̵��ܳɼ�(û�ɼ�����ʾΪ null )
SELECT s.Sid AS 'ѧ�����', s.Sname AS 'ѧ������',r.coursenumber AS 'ѡ������',r.scoresum AS '���пγ̵��ܳɼ�' FROM ( (SELECT Student.Sid,Student.Sname FROM Student )s LEFT JOIN (SELECT SC.Sid, SUM(SC.Score) AS scoresum, COUNT(SC.Cid) AS coursenumber FROM SC GROUP BY SC.Sid )r ON s.Sid = r.Sid )
--���гɼ���ѧ����Ϣ
SELECT * FROM Student WHERE Student.Sid IN (SELECT SC.Sid FROM SC)
--��ѯ�������ʦ������
SELECT COUNT(*) FROM Teacher WHERE Tname LIKE '��%'
--��ѯѧ������������ʦ�ڿε�ͬѧ����Ϣ
SELECT Student.* FROM Student,Teacher,Course,SC WHERE Student.Sid = SC.Sid AND Course.Cid=SC.Cid AND Course.Tid = Teacher.Tid AND Tname = '����'
--��ѯû��ѧȫ���пγ̵�ͬѧ����Ϣ
SELECT * FROM Student WHERE Student.Sid NOT IN (SELECT SC.Sid FROM SC GROUP BY SC.Sid HAVING COUNT(SC.Cid)= (SELECT COUNT(Cid) FROM Course))
--��ѯ������һ�ſ���ѧ��Ϊ" 01 "��ͬѧ��ѧ��ͬ��ͬѧ����Ϣ
SELECT * FROM Student WHERE Student.Sid IN (SELECT SC.Sid FROM SC WHERE SC.Cid IN(SELECT SC.Cid FROM SC WHERE SC.Sid = '1'))
--��ѯûѧ��"����"��ʦ���ڵ���һ�ſγ̵�ѧ������
SELECT * FROM Student WHERE Student.Sid NOT IN(SELECT SC.Sid FROM SC,Course,Teacher WHERE SC.Cid = Course.Cid AND Course.Tid = Teacher.Tid AND Teacher.Tname= '����')
--����" 01 "�γ̷���С�� 60���������������е�ѧ����Ϣ
SELECT Student.*, SC.Score FROM Student, SC WHERE Student.Sid = SC.Sid AND SC.Score < '60' AND Cid = '1' ORDER BY SC.Score DESC
--��ƽ���ɼ��Ӹߵ�����ʾ����ѧ�������пγ̵ĳɼ��Լ�ƽ���ɼ�
SELECT * FROM SC LEFT JOIN (SELECT Sid,AVG(Score) AS AVSCOLE FROM SC GROUP BY Sid )r ON SC.Sid = r.Sid ORDER BY AVSCOLE DESC
--��ѯ���Ƴɼ���߷֡���ͷֺ�ƽ����
SELECT SC.Cid , MAX(SC.Score) AS ��߷�, MIN(SC.Score) AS ��ͷ�, AVG(SC.Score) AS ƽ����, COUNT(*) AS ѡ������, SUM(CASE WHEN SC.Score>=60 THEN 1 ELSE 0 END )/COUNT(*)AS ������, 
SUM(CASE WHEN SC.Score>=70 AND SC.Score<80 THEN 1 ELSE 0 END )/COUNT(*)AS �е���, 
SUM(CASE WHEN SC.Score>=80 AND SC.Score<90 THEN 1 ELSE 0 END )/COUNT(*)AS ������, 
SUM(CASE WHEN SC.Score>=90 THEN 1 ELSE 0 END )/COUNT(*)AS ������ FROM SC GROUP BY SC.Cid ORDER BY COUNT(*)DESC, SC.Cid ASC 
--��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ�������ο�ȱ
DECLARE @id int
SET @id=(SELECT SId FROM Student WHERE SId=1)
WHILE(@id<19)
  BEGIN
    SELECT SC.SId,Sname,AVG(score) as 'ƽ���ɼ�' FROM SC INNER JOIN Student ON SC.SId=Student.SId WHERE @id=+1  GROUP BY SC.SId,Sname   ORDER BY AVG(score) DESC
    BREAK
  END
--��ѯ 1990 �������ѧ������
SELECT * FROM Student WHERE YEAR(Student.Sage)='1990'
--��ѯͬ��ͬ��ѧ����������ͳ��ͬ������
SELECT * FROM Student WHERE Sname IN ( SELECT Sname FROM Student GROUP BY Sname HAVING COUNT(*)>1 )
SELECT Sname, COUNT(*) FROM Student GROUP BY Sname HAVING COUNT(*)>1
--��ѯ�����к��С��硹�ֵ�ѧ����Ϣ
SELECT * FROM Student WHERE Student.Sname LIKE '%��%'
--��ѯ������Ů������
SELECT Ssex, COUNT(*) FROM Student GROUP BY Ssex
--��ѯ��ֻѡ�����ſγ̵�ѧ��ѧ�ź�����
SELECT Student.Sid, Student.Sname FROM Student WHERE Student.Sid IN 
(SELECT SC.Sid FROM SC GROUP BY SC.Sid HAVING COUNT(SC.Cid)=2)
--��ѯÿ�ſγ̱�ѡ�޵�ѧ����
SELECT Cid, COUNT(Sid) FROM SC GROUP BY Cid
--��ѯ���Ƴɼ�ǰ�����ļ�¼
SELECT * FROM SC WHERE (SELECT COUNT(*) FROM SC AS a WHERE SC.Cid = a.Cid AND SC.Score<a.Score)< 3 ORDER BY Cid ASC, SC.Score DESC
--�����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�������ο�ȱ
SELECT a.Cid, a.Sid, a.Score, COUNT(b.Score)+1 AS RANK FROM SC AS a LEFT JOIN SC AS b ON a.Score<b.Score AND a.Cid = b.Cid GROUP BY a.Cid, a.Sid,a.Score ORDER BY a.Cid, RANK ASC
--��ѯ��" 01 "�ŵ�ͬѧѧϰ�Ŀγ� ��ȫ��ͬ������ͬѧ����Ϣ
SELECT Student.* FROM Student WHERE Sid in (SELECT distinct SC.Sid FROM SC WHERE Sid <> '01' AND SC.Cid in (SELECT distinct Cid FROM SC WHERE Sid = '01') GROUP BY SC.Sid HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE Sid='01'))SELECT Student.* FROM Student WHERE Sid in
(SELECT distinct SC.Sid FROM SC WHERE Sid <> '01' AND SC.Cid in (SELECT distinct Cid FROM SC WHERE Sid = '01') GROUP BY SC.Sid HAVING COUNT(1) = (SELECT COUNT(1) FROM SC WHERE Sid='01'))
--��ѯ���ż������ϲ�����γ̵�ͬѧ��ѧ�ţ���������ƽ���ɼ�
SELECT Student.Sid , Student.Sname , CAST(AVG(Score) AS decimal(18,2)) AVG_Score FROM Student , SC WHERE Student.SID = SC.SID AND Student.Sid IN (SELECT Sid FROM SC WHERE Score < 60 GROUP BY Sid HAVING COUNT(1) >= 2) GROUP BY Student.Sid , Student.Sname
--�����Ƴɼ��������򣬲���ʾ������ Score �ظ�ʱ�ϲ�����
SELECT t.* , px = (SELECT COUNT(distinct Score) FROM SC WHERE Cid = t.Cid AND Score >= t.Score) FROM SC t order BY t.Cid , px
--��ѯѧ�����ܳɼ����������������ܷ��ظ�ʱ���������ο�ȱ
SELECT t.* , px = DENSE_RANK() OVER(ORDER BY �ܳɼ� DESC) FROM (SELECT m.Sid ѧ����� ,m.Sname ѧ������ ,isnull(SUM(Score),0) �ܳɼ� FROM Student m LEFT JOIN SC n ON m.Sid = n.Sid GROUP BY m.Sid , m.Sname) t ORDER BY px
--ͳ�Ƹ��Ƴɼ����������������γ̱�ţ��γ����ƣ�[100-85]��[85-70]��[70-60]��[60-0] ����ռ�ٷֱ�
SELECT Course.Cname, Course.Cid,
SUM(Case WHEN SC.Score<=100 AND SC.Score>85 THEN 1 ELSE 0 END) AS '[100-85]',
SUM(Case WHEN SC.Score<=85 AND SC.Score>70 THEN 1 ELSE 0 END) AS '[85-70]',
SUM(Case WHEN SC.Score<=70 AND SC.Score>60 THEN 1 ELSE 0 END) AS '[70-60]',
SUM(Case WHEN SC.Score<=60 AND SC.Score>0 THEN 1 ELSE 0 END) AS '[60-0]'
FROM SC LEFT JOIN Course
ON SC.Cid = Course.Cid
GROUP BY SC.Cid,Course.Cname,Course.Cid
--��ѯÿ�ſγ̵�ƽ���ɼ��������ƽ���ɼ��������У�ƽ���ɼ���ͬʱ�����γ̱����������
SELECT m.CiD , m.Cname , CAST(AVG(n.Score) AS DECIMAL(18,2)) AVG_Score FROM Course m, SC n WHERE m.CiD = n.CiD GROUP BY m.CiD , m.Cname ORDER BY AVG_Score DESC, m.CiD ASC
--��ѯƽ���ɼ����ڵ��� 85 ������ѧ����ѧ�š�������ƽ���ɼ�
SELECT a.SiD , a.Sname , CAST(AVG(b.score) AS DECIMAL(18,2)) AVG_Score FROM Student a , SC b WHERE a.SiD = b.SiD GROUP BY a.SiD , a.Sname HAVING CAST(AVG(b.score) AS DECIMAL(18,2)) >= 85 ORDER BY a.SiD
--��ѯ�γ�����Ϊ����ѧ�����ҷ������� 60 ��ѧ�������ͷ���
SELECT Sname , Score FROM Student , SC , Course WHERE SC.SiD = Student.SiD AND SC.CiD = Course.CiD AND Course.Cname = N'��ѧ' AND Score < 60
--��ѯ����ѧ���Ŀγ̼��������������ѧ��û�ɼ���ûѡ�ε������
SELECT Student.* , Course.Cname , SC.CiD , SC.Score FROM Student, SC , Course WHERE Student.SiD = SC.SiD AND SC.CiD = Course.CiD ORDER BY Student.SiD , SC.CiD
--��ѯ�κ�һ�ſγ̳ɼ��� 70 �����ϵ��������γ����ƺͷ���
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score >= 70 ORDER BY Student.SID , SC.CID
--��ѯ������Ŀγ�
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.score < 60 ORDER BY Student.SID , SC.CID
--��ѯ�γ̱��Ϊ 01 �ҿγ̳ɼ��� 80 �����ϵ�ѧ����ѧ�ź�����
SELECT Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course WHERE Student.SID = SC.SID and SC.CID = Course.CID and SC.CID = '1' and SC.score >= 80 ORDER BY Student.SID , SC.CID
--��ÿ�ſγ̵�ѧ������
SELECT Course.CID , Course.Cname , count(*) ѧ������ FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname ORDER BY Course.CID , Course.Cname
--�ɼ����ظ�����ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�
SELECT top 1 Student.* , Course.Cname , SC.CID , SC.score FROM Student, SC , Course , Teacher WHERE Student.SID = SC.SID and SC.CID = Course.CID and Course.TID = Teacher.TID and Teacher.Tname = N'����' ORDER BY SC.score DESC
--�ɼ����ظ�������£���ѯѡ�ޡ���������ʦ���ڿγ̵�ѧ���У��ɼ���ߵ�ѧ����Ϣ����ɼ�
SELECT student.*, sc.score, sc.cid FROM student, teacher, course,sc 
WHERE teacher.tid = course.tid
AND sc.sid = student.sid
AND sc.cid = course.cid
AND teacher.tname = '����'
AND sc.score = (
    SELECT Max(sc.score) 
    FROM sc,student, teacher, course
    WHERE teacher.tid = course.tid
    AND sc.sid = student.sid
    AND sc.cid = course.cid
    AND teacher.tname = '����'
)
--��ѯ��ͬ�γ̳ɼ���ͬ��ѧ����ѧ����š��γ̱�š�ѧ���ɼ�
SELECT m.* FROM SC m ,(SELECT CID , score FROM SC GROUP BY CID , score having count(1) > 1) n WHERE m.CID= n.CID and m.score = n.score ORDER BY m.CID , m.score , m.SID
--��ѯÿ�Ź��ɼ���õ�ǰ����
SELECT t.* FROM sc t WHERE score in (SELECT top 2 score FROM sc WHERE CID = T.CID ORDER BY score DESC) ORDER BY t.CID , t.score DESC
--ͳ��ÿ�ſγ̵�ѧ��ѡ������������ 5 �˵Ŀγ̲�ͳ�ƣ���
SELECT Course.CID , Course.Cname , count(*) ѧ������ FROM Course , SC WHERE Course.CID = SC.CID GROUP BY Course.CID , Course.Cname having count(*) >= 5 ORDER BY ѧ������ DESC , Course.CID
--��������ѡ�����ſγ̵�ѧ��ѧ��
SELECT student.SID , student.Sname FROM student , SC WHERE student.SID = SC.SID GROUP BY student.SID , student.Sname having count(1) >= 2 ORDER BY student.SID
--��ѯѡ����ȫ���γ̵�ѧ����Ϣ
SELECT student.* FROM student WHERE SID in(SELECT SID FROM sc GROUP BY SID HAVING count(1) = (SELECT count(1) FROM course))
--��ѯ��ѧ�������䣬ֻ���������
SELECT * , DATEDIFF(yy , sage , GETDATE()) ���� FROM student
--���ճ����������㣬��ǰ���� < �������µ������������һ
SELECT * , case when right(convert(VARCHAR(10),GETDATE(),120),5) < right(convert(VARCHAR(10),sage,120),5) then DATEDIFF(yy , sage , GETDATE()) - 1 else DATEDIFF(yy , sage , GETDATE()) end ���� FROM student
--��ѯ���ܹ����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0
--��ѯ���ܹ����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(week,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1
--��ѯ���¹����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = 0
--��ѯ���¹����յ�ѧ��
SELECT * FROM student WHERE DATEDIFF(mm,datename(yy,GETDATE()) + right(convert(VARCHAR(10),sage,120),6),GETDATE()) = -1