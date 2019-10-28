CREATE table LeaveTable (
序號 int unsigned auto_increment primary key,
員編 char(50) not null default '',
開始日期 date not null default '2000-00-00',
開始時間 time not null default '00:00:00',
結束日期 date not null default '2000-00-00',
結束時間 time not null default '00:00:00',
假別  char(50)  not null default '',
假別子項目  char(50)  not null default '',
送審主管  char(50) not null default ''
)engine myisam charset utf8mb4;