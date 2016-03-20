My Project has three different parts:
1 - The ASP.net Website codded in VB.net and HTML
2 - The Database codded in SQL and T-SQL
3 - The Minecraft Bukkit Plugin codded in Java

Each part is stored in a separate folder in the same directory as this file.

=ASP.net Server Code=
This code is for the website.
Files in this folder:

Death.vb
Map.vb
MapList.aspx
MapList.aspx.vb
MapProfile.aspx
MapProfile.aspx.vb
Player.vb
PlayerList.aspx
PlayerList.aspx.vb
PlayerMapProfile.aspx
PlayerMapProfile.aspx.vb
PlayerMapStats.vb
PvP.vb
Site.Master
Site.Master.vb
SQLdbConnection.vb

The files that follow the format Name.vb are classes written in VB.net.
The files that follow the format Name.aspx.vb are webpages written in VB.net.
The files that follow the format Name.aspx are webpages written in HTML.
Each .aspx has a matching .aspx.vb, this is because every webpage has both VB.net and HTML sides to it.
The file Site.Master and Site.Master.vb are the code for the master webpage which provides a template for
.aspx and .aspx.vb files to inherit.

=Database Code=
The Database Code directory has two directories:

-=Tables=-
Files in this directory:

dbo.Death.sql
dbo.Map.sql
dbo.Player.sql
dbo.PlayerMapStats.sql
dbo.PvP.sql

This is the SQL code used to create all five of the database tables used in this project.

-=Stored Procedures=-
Files in this directory:

dbo.AddPvP.sql
dbo.IncrementBlazeKills.sql
dbo.IncrementCaveSpiderKills.sql
dbo.IncrementCreeperKills.sql
dbo.IncrementEndermanKills.sql
dbo.IncrementGhastKills.sql
dbo.IncrementMagmaCubeKills.sql
dbo.IncrementSilverfishKills.sql
dbo.IncrementSkeletonKills.sql
dbo.IncrementSlimeKills.sql
dbo.IncrementSpiderKills.sql
dbo.IncrementWitchKills.sql
dbo.IncrementWitherKills.sql
dbo.IncrementWitherSkeletonKills.sql
dbo.IncrementWolfKills.sql
dbo.IncrementZombieKills.sql
dbo.IncrementZombiePigmanKills.sql
dbo.KillEnderDragon.sql
dbo.NewMap.sql
dbo.PlayerLogin.sql
dbo.PlayerLogout.sql
dbo.PlayerMapSwitch.sql
dbo.RecordDeath.sql
dbo.ResetPlayerMapStats.sql
dbo.SwitchMap.sql

This is the stored procedures that are called upon by the bukkit plugin written in T-SQL, each stored procedure
manipulates data in the database. The reason these exists is to allow other programs to run these functions such
as my ASP.net website.

=Bukkit Plugin Code=
Firstly an explanation, Bukkit Minecraft is a modded version of Minecraft. There also exists a Java API
called Bukkit, these combined allow the easy integration into the game. These programs were not written by me.
I also use a SQL API and I use a driver to connect to my database. The code i have written included in the
directory is:

BUAP.class
BUAP.java
BUAPListener.class
BUAPListener.java
out.javadesc
plugin.yml
SQLConnect.class
SQLConnect.java

The .class and .java are simply java classes. SQLConnect is supposed to handle database queries and
updates. BUAP is supposed to handle user input commands. BUAPListener is supposed to run code in reaction
to certain in game events such as a player disconnecting.

The .javadesc is the code I used to export the project using eclipse.

The plugin.yml is used by the bukkit minecraft to give error messages and supply plugin information.

Just to clarify the term plugin refers to a Minecraft mod that uses an API and a Mod together to achieve
Its goals.