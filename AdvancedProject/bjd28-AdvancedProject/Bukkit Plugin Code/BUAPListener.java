package me.bjd28.BUAP;

import org.bukkit.entity.EntityType;
import org.bukkit.entity.Player;
import org.bukkit.entity.Skeleton;
import org.bukkit.entity.Skeleton.SkeletonType;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDeathEvent;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;

public class BUAPListener implements Listener {
	private SQLConnect SQLConn;
	
	//To ensure that the code for an entity death isn't run when a player dies this boolean value is used within PlayerDeath and MobKill
	private boolean isPlayerDeath = false; 
	
	public BUAPListener(SQLConnect SQLConn){
		this.SQLConn = SQLConn;
	}
	
	@EventHandler
	public void PlayerLogin(PlayerJoinEvent event)
	//This is run when a player logs in, the required table entries are entered in to the database if they are new to the server or map
	//They are marked to be online.
	{
		Player LoginPlayer = event.getPlayer();
		String PlayerName = LoginPlayer.getDisplayName();
		LoginPlayer.sendMessage("Hello " + PlayerName);
		SQLConn.SingleArgumentEvent("PlayerLogin", PlayerName);
		System.out.println("BUAP: LogIn Event Called");
	}
	
	@EventHandler
	public void PlayerLogout(PlayerQuitEvent event)
	//This is run when a player logs out, the player is marked at logged out and the date of log out is stored
	{
		String LogoutPlayerName = event.getPlayer().getDisplayName();
		SQLConn.SingleArgumentEvent("PlayerLogout", LogoutPlayerName);
		System.out.println("BUAP: LogOut Event Called");
	}
	
	@EventHandler
	public void PlayerDeath(PlayerDeathEvent event)
	//This is run when a player dies, if it is by another player it is recorded in the PVP database otherwise the death log is taken
	{
		isPlayerDeath = true;
		Player DeadPlayer = event.getEntity();
		String DeadPlayerName = DeadPlayer.getDisplayName();
		Player Killer = DeadPlayer.getKiller();
		if (Killer == null) //Kiler is null when the player died by means other than a player
		{
			String DeathLog = event.getDeathMessage();
			SQLConn.DoubleArgumentEvent("RecordDeath", DeadPlayerName, DeathLog);
		}
		else //Death by pvp
		{
			String KillerName = Killer.getDisplayName();
			SQLConn.DoubleArgumentEvent("AddPvP", DeadPlayerName, KillerName);
		}
	}	
	@EventHandler
	public void MobKill(EntityDeathEvent event)
	//This is run when an entity dies, if the killer is a player and the entity is not a player then the stat is recorded in the database
	{
		if (isPlayerDeath == false) //If the entity is not a player...
		{
			Player Killer = event.getEntity().getKiller();
			if (Killer != null) //... and If the killer is a player run record death
			{
				//System.out.println(Killer.getDisplayName() + " killed " + event.getEntityType().toString());
				EntityType MobType = event.getEntityType(); //this is an enum for all entities in the game
				String KillerName = Killer.getDisplayName();
				switch (MobType) //Here is every mob supported by my system
				{
				case CAVE_SPIDER:
					IncrementMobKill(KillerName, "CaveSpider");
					break;
				case ENDERMAN:
					IncrementMobKill(KillerName, "Enderman");
					break;
				case SPIDER:
					IncrementMobKill(KillerName, "Spider");
					break;
				case WOLF:
					IncrementMobKill(KillerName, "Wolf");
					break;
				case PIG_ZOMBIE:
					IncrementMobKill(KillerName, "ZombiePigman");
					break;
				case BLAZE:
					IncrementMobKill(KillerName, "Blaze");
					break;
				case CREEPER:
					IncrementMobKill(KillerName, "Creeper");
					break;
				case GHAST:
					IncrementMobKill(KillerName, "Ghast");
					break;
				case MAGMA_CUBE:
					IncrementMobKill(KillerName, "MagmaCube");
					break;
				case SILVERFISH:
					IncrementMobKill(KillerName, "Silverfish");
					break;
				case SKELETON:
					//There are two different types of skeleton: Normal & Wither
					//To work out which is the one in queestion we need to change entity to type Skeleton
					Skeleton mob = (Skeleton) event.getEntity();
					if (mob.getSkeletonType() == SkeletonType.NORMAL)
					{
						IncrementMobKill(KillerName, "Skeleton");
					}
					else
					{
						IncrementMobKill(KillerName, "WitherSkeleton");
					}
					break;
				case SLIME:
					IncrementMobKill(KillerName, "Slime");
					break;
				case WITCH:
					IncrementMobKill(KillerName, "Witch");
					break;
				case ZOMBIE:
					IncrementMobKill(KillerName, "Zombie");
					break;
				case WITHER:
					IncrementMobKill(KillerName, "Wither");
					break;
				case ENDER_DRAGON:
					SQLConn.SingleArgumentEvent("KillEnderDragon", KillerName);
				default:
					break; //There are other mobs in game that I've decided I don't care to record
				}
			}
		}
		isPlayerDeath = false;
	}
	
	//This function is used for Incrementing Mob kills given a PlayerName & a Mob Name
	private void IncrementMobKill(String PlayerName, String MobName) 
	{
		SQLConn.SingleArgumentEvent("Increment" + MobName + "Kills", PlayerName);
	}	
}
		
	
	

