using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web.Hosting;
using System.Web.Http;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class PlayerController : ApiController
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Player> _players = new List<Player>();

        public PlayerController()
        {
            PlayersFile = HostingEnvironment.MapPath(@"/App_Data/Players.txt");
            WriteFileToList(_players, PlayersFile);
        }

        private string PlayersFile { get; }

        public IHttpActionResult DeletePlayer(string field, string search)

        {
            int itemsRemoved=0;
            bool found = false;

            switch (field)
            {
                case "Name":
                {
                   itemsRemoved= _players.RemoveAll(p => p.Player_name.ToLower().Contains(search.ToLower()));
                    found = (itemsRemoved > 0);
                    break;
                }
                case "Registration_ID":
                    {
                        itemsRemoved = _players.RemoveAll(p => p.Registration_ID.ToLower().Contains(search.ToLower()));
                        found = (itemsRemoved > 0);
                        break;
                    }
                default:
                    {
                        return NotFound();
                    }
            }

            if (found)
            {
            //candidate to move to event listner
                ClearFileContents(PlayersFile);
                WriteListToFile(_players, PlayersFile);
                return Ok(_players);
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetAllPlayers()
        {
            return Ok(_players);
        }

        public IHttpActionResult GetAllPlayers(string field,string search)
        {
            switch (field)
            {
                case "Name":
                {
                    return Ok(_players.Where(p => p.Player_name.ToLower().Contains(search.ToLower())));
                }
                case "Registration_ID":
                    {
                        return Ok(_players.Where(p => p.Registration_ID.ToLower().Contains(search.ToLower())));
                    }
                default:
                    {
                        return NotFound();
                    }
            }
        }

        public IHttpActionResult GetPlayerInfo(string Registration_ID)
        {
            var player = _players.FirstOrDefault((p) => p.Registration_ID == Registration_ID);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);

        }

        public IHttpActionResult PostPlayer(Player plr)
        {
            var player = _players.FirstOrDefault((p) => p.Registration_ID == plr.Registration_ID);
            if (player != null)
            {
                _players.Remove(player);
                _players.Add((plr));
            }
            else
            {
                _players.Add((plr));
            }
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

        private static bool WriteListToFile(IEnumerable<Player> playerlist, string filename)
        {
            using (var file = new StreamWriter(filename))
            {
                foreach (var player in playerlist)
                {
                    {
                        string format = "yyyy-MM-dd";   // Use this format.
                       
                        string line = $"{player.Registration_ID},{player.Player_name},{player.Team_name}, {(player.Date_of_Birth.Date.ToString(format))}";
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
                Date_of_Birth =Convert.ToDateTime(result[3])
            };
            return newPlayer;
        }
    }
}
