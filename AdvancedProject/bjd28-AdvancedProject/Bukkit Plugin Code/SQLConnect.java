package me.bjd28.BUAP;
import java.sql.*; //This Module contains SQL query and update constructors & executors

import org.bukkit.Server;
import org.bukkit.entity.Player;

public class SQLConnect {
	static private Connection conn = null;
	
	//Constructor class establishes connection with my database and specifies the database table
	public SQLConnect(){
		Connection connA = null;
		try {
			//This is the connection string for my database
            String connURL = "jdbc:sqlserver://localhost;instance=SQLEXPRESS;"
                    + "integratedSecurity=true;"; 
            try{
            	//SQL database driver
            	Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
            }
            catch (ClassNotFoundException ex){
            	System.out.println("SQL: Driver not found.");
            }
            //Establish Connection
            connA = DriverManager.getConnection(connURL);
            //Specify Database table to be used
            String usedb = "USE BathCSAdvancedProject;";
            Statement stmt = connA.createStatement();
            stmt.execute(usedb);
            stmt.close();
        }
        
        catch (SQLException ex) {
           System.out.println("SQL: Unable to Connect to Database.");
           }
		conn = connA;
	}
			
	//Triggers a stored procedure with a single argument with a given procedure name
	public void SingleArgumentEvent(String ProcedureName, String Arg1)
	{
		try {   
			PreparedStatement psmt = conn.prepareStatement("{call dbo." + ProcedureName +"(?)}"); //Creates name, the (?) is the parameter
	          psmt.setString(1, Arg1); //sets string as parameter
	          psmt.execute(); //execute procedure
	          psmt.close();
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: Signle Player Event");
	      }
	}
	
	//Trigers a stored procedure with two arguments with a given procedure name
	public void DoubleArgumentEvent(String ProcedureName, String Arg1, String Arg2)
	{
		try {
			PreparedStatement psmt = conn.prepareStatement("{call dbo." + ProcedureName +"(?, ?)}"); //This has two parameters instead
	        psmt.setString(1, Arg1); //set first parameter
	        psmt.setString(2, Arg2);//set second parameter
	        psmt.execute();//execute procedure
	        psmt.close();
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: Double Player Event");
	      }
	}
	
	//Checks is a map exists with the given mapID
	private boolean doesMapIDExsist(int MapID)
	{
		try {            
	          Statement stmt = conn.createStatement(); 
	        //Retrieves information from Map database table where the mapID matches its input
	          ResultSet rs = stmt.executeQuery("Select MapID from Map where MapID = " + MapID); 
	          if (rs.next()){
	        	  return true; //If there is data with that mapID return true
	          }
	          stmt.close();
	      }        
	      catch (SQLException ex) {
	         System.out.println("Fail doesMapIDExsist");
	      }
		return false; //If no matching data is found return false
	}

	//Checks whether or not there is an active map
	public boolean isActiveMap()
	{
		try {            
	          Statement stmt = conn.createStatement(); 
	          ResultSet rs = stmt.executeQuery("Select MapID from Map where isActive = 1"); //Select data of the active map
	          if (rs.next()){
	        	  return true; //If a match is found return true
	          }
	          stmt.close();
	      }        
	      catch (SQLException ex) {
	         System.out.println("Fail isActiveMap");
	      }
		return false; //If no match is found then there is no active map so return false
	}


	//Creates a new map by calling the NewMap stored procedure and then assigning all online players with new Map-Player Statistics
	public int NewMap()
	{
		try {
			//Calls database procedure NewMap, this procedure has no arguments
			PreparedStatement psmt = conn.prepareStatement("{call dbo.NewMap()}"); 
  	        psmt.execute();
  	        psmt.close();
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: New Map Command");
	    	  return -1;
	      }
		
		int MapID = -1;
		try {
			Statement stmtA;
			stmtA = conn.createStatement();
			ResultSet rs = stmtA.executeQuery("Select MapID from Map Where isActive = 1");
			//Retrieve the MapID of the currently Active Map from the database and store it in MapID
			if (rs.next())
			{
				MapID = rs.getInt(1);
			}
			
			stmtA = conn.createStatement();
			//Select the playerID of all players that are online from the database
			rs = stmtA.executeQuery("Select PlayerID from Player where isOnline = 1");
			while (rs.next())
			{
				Statement stmtB;
				stmtB = conn.createStatement();
				//For every player online add a new PlayerMap stats row using their PlayerID and the Active Map ID
				stmtB.executeUpdate("Insert into PlayerMapStats values(" + MapID + ", " 
						+ rs.getInt(1) + ", 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0)");
				
			}
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: Map Data Update Command");
	    	  return -1;
	      }
		return MapID;
	}
	
	//Calls the switch map stored procedure giving it an integer MapID, it then registers all online players who have no Player-Map Stats for the newly
	//Switched map with Player-map stats
	public String SwitchMap(int MapID)
	{
		if (doesMapIDExsist(MapID) == false) //If no map is found the switch cannot occur
		{
			return "There is no Map with that ID";
		}
		
		try {
			//Call SwitchMap on database, this deactivates the current map and activates the new map
			PreparedStatement psmt = conn.prepareStatement("{call dbo.SwitchMap(?)}");
			psmt.setInt(1, MapID); //Pass MapID as an argument
  	        psmt.execute();
  	        psmt.close();
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: Switch Map Command");
	    	  return "Fail: Switch Map Command";
	      }
		
		
		try {
			Statement stmt = conn.createStatement();
			//Get the PlayerID of all players that are currently online
			ResultSet rs = stmt.executeQuery("Select PlayerID from Player where isOnline = 1");
			while (rs.next())
			{
				//For every player call PlayerMapSwitch which creates a new PlayerMap stats row on the newly activated map if one did not exsist already
				PreparedStatement psmt = conn.prepareStatement("{call dbo.PlayerMapSwitch(?, ?)}");
				psmt.setInt(1, rs.getInt(1)); //Passes the PlayerID
				psmt.setInt(2, MapID); //Passes the current MapID
	  	        psmt.execute();
	  	        psmt.close();
				
			}
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: Switch Map - PlayerMap Update Command");
	    	  return "Fail: Switch Map - PlayerMap Update Command";
	      }
		
		return ("Map has been switched to Map #" + MapID);
		
	}
	
	//Deletes map data with a specified mapID, it will not do so and return error message if such a map is non-exsistant or currently active
	public String deleteMapData(int MapID)
	{
		if (doesMapIDExsist(MapID) == false) //If no map is found the delete cannot occur
		{
			return "There is no Map with that ID";
		}
		
		
		try{
			Statement stmt;
			stmt = conn.createStatement();
			//Gets the isActive boolean from the specified map that the user wishes to delete
			ResultSet rs = stmt.executeQuery("Select isActive from Map where MapID = " + MapID);
			rs.next();
			//If the map is active it cannot be deleted
			if (rs.getBoolean(1) == true)
			{
				return "Cannot delete currently active map";
			}
			stmt.close();
		}
		catch (SQLException ex) {
			System.out.println("Fail isActive check - delete map");
		}
		
		//Clear all mapstats related to the map and all deaths and pvp records related to any mapstats
		WipeMapRelatedData(MapID);
		
		try{
			Statement stmt;
			stmt = conn.createStatement();
			stmt.executeUpdate("Delete from Map where MapID = " + MapID); //This deletes the actual map
			stmt.close();
		}
		catch (SQLException ex) {
			System.out.println("Fail delete Map Data");
		}
		return "The Map was deleted successfully";
		
		
	}

	//Resets all map data of a map with the given id, it will have data identicle to a brand new map
	public String resetMapData(int MapID)
	{
		if (doesMapIDExsist(MapID) == false) //If map does not exist this is not a valid command
		{
			return "There is no Map with that ID";
		}
		
		WipeMapRelatedData(MapID); //Clear data
		return ("Map #" + MapID + " has been reset.");
		
	}

	//finds all 
	private void WipeMapRelatedData(int MapID)
	{
		
		try {            
	          Statement stmt = conn.createStatement();
	          //Compound select statement that takes data out of Player, PlayerMapStats and Map
	          ResultSet rs = stmt.executeQuery("Select PlayerMapStats.PlayerMapID, Player.isOnline, Map.isActive " + 
	          "from PlayerMapStats join Player on PlayerMapStats.PlayerID = Player.PlayerID "
	          + "join Map on PlayerMapStats.MapID = Map.MapID where Map.MapID = " + MapID);
	          
	          while (rs.next()){
	        	  //Loops for each player
	        	int PlayerMapID = rs.getInt(1);
	        	boolean isPlayerOnMap = rs.getBoolean(2) && rs.getBoolean(3); //This is true if the player is online and the map in question is active
	        	DeletePlayerMapStatsRow(PlayerMapID, isPlayerOnMap); //Deletes player map data for the player of this loop
	          }
	
	          
	      }        
	      catch (SQLException ex) {
	         System.out.println("Fail WipeMapRelatedData");
	      }
	}

	//Calls stored procedure on database that deletes player map stats with a given ID, including all death and pvp logs
	private void DeletePlayerMapStatsRow(int PlayerMapID, boolean isPlayerOnMap)
	{
		try {            
	        	PreparedStatement psmt = conn.prepareStatement("{call dbo.ResetPlayerMapStats(?, ?)}"); //Function name with 2 arguments
	  	        psmt.setInt(1, PlayerMapID); //Sets PlayerMapID as first argument
	  	        psmt.setBoolean(2, isPlayerOnMap); //Sets booleans as second argument
	  	        psmt.execute();
	  	        psmt.close();          
	      }        
	      catch (SQLException ex) {
	         System.out.println("Fail DeletePlayerMapStatsRow");
	      }
	}

	//Deletes a player with a given player name
	public String deletePlayerData(String PlayerName){
		int PlayerID = -1;
		try {
				Statement stmtA;
				stmtA = conn.createStatement();
				ResultSet rs = stmtA.executeQuery("Select isOnline, PlayerID from Player Where Name = '" + PlayerName + "'");
				if (rs.next())
				{
					if (rs.getBoolean(1)) //If the player is online return error message
					{
						return "Cannot delete online player, please kick them before deleting them.";
					}
					PlayerID = rs.getInt(2);
				}
				else //If there is no player return error message
				{
					return "Player not found.";
				}
			}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: SQL Error - Online Check");
	    	  return "SQL Error - Online Check";
	      }
		
		
		try {
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery("Select PlayerMapID from PlayerMapStats where PlayerID = " + PlayerID);
			while (rs.next())
			{
				//Delete Player related statistics
				int PlayerMapID = rs.getInt(1);
				DeletePlayerMapStatsRow(PlayerMapID, false);
			}
		}        
	      catch (SQLException ex) {
	    	  System.out.println("Fail: SQL Error - Player Stats Delete");
	    	  return "SQL Error - Player Stats Delete";
	      }
		
		try {
			Statement stmt = conn.createStatement();
			//Delete player field in database
			stmt.executeUpdate("Delete from Player where PlayerID = " + PlayerID);
		}
		
	    catch (SQLException ex) {
	    	  System.out.println("Fail: SQL Error - Player Delete");
	    	  return "SQL Error - Player Delete";
	      }
	    return ("Player " + PlayerName + " Deleted");
		
	      }
	
	//Takes all online players and logs them in
	public void ReLogPlayers(Server server)
	{
		Player[] listOfOnlinePlayers = server.getOnlinePlayers(); //Gets list of online players
		int NoOfOnlinePlayers = listOfOnlinePlayers.length;
		if (NoOfOnlinePlayers > 0)
		{
			for (int i=0; i >= NoOfOnlinePlayers; i++)
			{
				//Runs login stored procedure using every players name in the list of online players
				SingleArgumentEvent("PlayerLogin", listOfOnlinePlayers[i].getDisplayName()); 
			}
		}
	}
	
	//Sets all players so that they are offline
	public void logOutAllPlayers()
	{
		try {
			Statement stmt = conn.createStatement();
			//With no where command all players have isOnline set to false
			stmt.executeUpdate("Update Player set isOnline = 0");
		}
		
	    catch (SQLException ex) {
	    	  System.out.println("Fail: logOutAllPlayers");
	      }
	}
}
	



