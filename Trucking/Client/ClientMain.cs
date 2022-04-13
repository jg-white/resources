using System;
using System.Collections.Generic;
using CitizenFX.Core;
using System.Threading;
using static CitizenFX.Core.Native.API;

namespace Trucking.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            //drawJobMarker();
        }

        /*private void drawJobMarker()
        {
            while (true)
            {
                DrawMarker(39, 123, 64061, 33, 0, 0, 0, 0, 0, 0, 0, 2, 2, 93, 182, 229, 255,
                    false, true, 2, true, null, null, false);
            }
        }*/
        
        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("car", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                // account for the argument not being passed
                var model = "phantom";
                if (args.Count > 0)
                {
                    model = args[0].ToString();
                }

                // check if the model actually exists
                // assumes the directive `using static CitizenFX.Core.Native.API;`
                var hash = (uint) GetHashKey(model);
                if (!IsModelInCdimage(hash) || !IsModelAVehicle(hash))
                {
                    TriggerEvent("chat:addMessage", new 
                    {
                        color = new[] { 255, 0, 0 },
                        args = new[] { "[CarSpawner]", $"It might have been a good thing that you tried to spawn a {model}. Who even wants their spawning to actually ^*succeed?" }
                    });
                    return;
                }

                // create the vehicle
                var vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);
    
                // set the player ped into the vehicle and driver seat
                //Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // tell the player
                TriggerEvent("chat:addMessage", new 
                {
                    color = new[] {255, 0, 0},
                    args = new[] {"[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!"}
                });
            }), false);
        }
    }
}