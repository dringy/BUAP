package me.bjd28.BUAP;

import java.util.logging.Logger;

import org.bukkit.Server;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.plugin.PluginDescriptionFile;
import org.bukkit.plugin.PluginManager;
import org.bukkit.plugin.java.JavaPlugin;

//Constructor Class
public class BUAP extends JavaPlugin{
	SQLConnect SQLConn = null;
	public final Logger logger = Logger.getLogger("Minecraft");
	//Registers itself as plugin
	public static BUAP plugin; 

	//Ondisable occurs when server is shut down, or plugin is closed by another plgin
	public void onDisable() {
		PluginDescriptionFile pdfFile = this.getDescription();
		this.logger.info(pdfFile.getName() + " Has Been Disabled.");
		SQLConn.logOutAllPlayers(); //Marks all players as offline
	}
	
	//Enable occurs when plugin is activated, usually by booting the server
	public void onEnable() {
		SQLConn = new SQLConnect(); //Creates SQLConnect object
		PluginDescriptionFile pdfFile = this.getDescription();
		this.logger.info(pdfFile.getName() + " Version " + pdfFile.getVersion() + " Has Been Enabled.");
		PluginManager manager = this.getServer().getPluginManager();
		manager.registerEvents(new BUAPListener(SQLConn), this);
		Server server = getServer();
		
		if (!SQLConn.isActiveMap()) //If there is no active map, make a new map.
		{
			SQLConn.NewMap();
		}
		
		//In the event the plugin is booted after the server is booted (via another plugin) all players need to be logged to ensure online players are marked online
		SQLConn.ReLogPlayers(server);
	}
	
	//This function has a command label "BUAP" which is the start of all supporting commands in this plugin
	//The way this function works is if false is returned a message is displayed which I specified in plugin.yml
	//otherwise Bukkit will not react when true is returned
	public boolean onCommand(CommandSender sender, Command cmd, String commandLabel, String[] args){
		if(commandLabel.equalsIgnoreCase("BUAP")){
			if (args.length > 0) //If there is at least one argument check the argument for a matching command
			{
				switch (args[0].toUpperCase())
				{
				case "NEW": //BUAP NEW
					int MapID = SQLConn.NewMap();
					sender.sendMessage("New Map Added: Map #" + MapID);
					return true;
				case "DELETEMAP": //BUAP DELETEMAP [MAPID]
					return DeleteMap(args, sender);
				case "RESET": //BUAP RESET [MAPID]
					return ResetMap(args, sender);
				case "DELETEPLAYER": //BUAP DELETEPLAYER [PLAYERNAME]
					return DeletePlayer(args, sender);
				case "MAPSWITCH": //BUAP MAPSWITCH [MAPID]
					return SwitchMap(args, sender);
				default:
					return false; //if the argument is not recognised return false
				
				}
			}
		}
		return false; //This case if for /BUAP which has no meaning on its own
	}
	
	
	private boolean ResetMap(String[] args, CommandSender sender)
	{
		if (args.length < 2) //This command requires an additional argument, if non is provided the command was used incorrectly
		{
			return false;
		}
		
		//We need to make their input string into an integer, if it does not work the string must not be a number so the command was used incorrectly
		int MapID = -1;
		try
		{
			MapID = Integer.parseInt(args[1]);
		}
		catch (NumberFormatException exc)
		{
			return false;
		}
		
		//If there has been an error, a message will be returned, otherwise a success message should be displayed
		sender.sendMessage(SQLConn.resetMapData(MapID));
		return true;
		//Although true is returned the command may not have been successful but an error message should appear from the SQLConn function
	}
	
	private boolean DeleteMap(String[] args, CommandSender sender)
	{
		if (args.length < 2) //This command requires an additional argument, if non is provided the command was used incorrectly
		{
			return false;
		}
		
		int MapID = -1;
		//We need to make their input string into an integer, if it does not work the string must not be a number so the command was used incorrectly
		try
		{
			MapID = Integer.parseInt(args[1]); 
		}
		catch (NumberFormatException exc)
		{
			return false;
		}
		
		//If there has been an error, a message will be returned, otherwise a success message should be displayed
		sender.sendMessage(SQLConn.deleteMapData(MapID));
		return true;
		//Although true is returned the command may not have been successful but an error message should appear from the SQLConn function
	}
	
	private boolean DeletePlayer(String[] args, CommandSender sender)
	{
		if (args.length < 2) //This command requires an additional argument, if non is provided the command was used incorrectly
		{
			return false;
		}
		
		String PlayerName = args[1]; //PlayerName is taken out of argument
		
		//If there has been an error, a message will be returned, otherwise a success message should be displayed
		sender.sendMessage(SQLConn.deletePlayerData(PlayerName)); 
		return true; 
		//Although true is returned the command may not have been successful but an error message should appear from the SQLConn function
	}
	
	private boolean SwitchMap(String[] args, CommandSender sender)
	{
		if (args.length < 2) //This command requires an additional argument, if non is provided the command was used incorrectly
		{
			return false;
		}
		
		//We need to make their input string into an integer, if it does not work the string must not be a number so the command was used incorrectly
		int MapID = -1;
		try
		{
			MapID = Integer.parseInt(args[1]);
		}
		catch (NumberFormatException exc)
		{
			return false;
		}
		
		//If there has been an error, a message will be returned, otherwise a success message should be displayed
		sender.sendMessage(SQLConn.SwitchMap(MapID));
		return true;
		//Although true is returned the command may not have been successful but an error message should appear from the SQLConn function
	}


}

