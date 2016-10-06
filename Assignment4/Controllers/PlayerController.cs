using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class PlayerController : ApiController
    {
        private  const string PlayersFile="~/App_Data/Players.txt";
        private List<Player> players = new List<Player>();

        public IHttpActionResult DeletePlayer(string id)
        {
            return Ok();
        }

        public IHttpActionResult GetAllPlayers()
        {
            return Ok();
        }

        public IHttpActionResult GetPlayerInfo(string field, string Value)
        {
            return Ok();
        }
        public IHttpActionResult PostPlayerRegistration(string Registration_ID,string Player_name,string Team_name,DateTime Date_of_Birth)
        {
            return Ok();
        }

        private static bool ClearFileContents(string filename)
        {
            try
            {
                File.WriteAllText(filename, string.Empty);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("error " + e.StackTrace);
            }
            return true;
        }

        private static bool WriteListToFile(IEnumerable<Player> Playerlist, string filename)
        {
            using (var file = new StreamWriter(filename))
            {
                foreach (var player in Playerlist)
                {
                    {
                        string line = $"{player.Registration_ID},{player.Player_name},{player.Team_name},{player.Date_of_Birth}";
                        file.WriteLine(line);
                    }
                }
                file.Close();
            }
            return true;
        }

        private static bool WriteFileToList(ICollection<Player> playerlist, string filename)
        {
            StreamReader file = StreamReader.Null;
            try
            {
                file = new StreamReader(filename);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] result = line.Split(',');
                    playerlist.Add(CreatePlayer(result));
                }
            }
            catch (Exception e)

            {
                System.Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                file.Close();
            }
            return true;
        }

        private static Player CreatePlayer(IReadOnlyList<string> result)
        {
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Player newPlayer = new Player
            {
                Registration_ID = result[0],
                Player_name = result[1],
                Team_name = result[2],
                Date_of_Birth = Convert.ToDateTime(result[3])
            };
            return newPlayer;
        }
    }
}
