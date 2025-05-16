using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DotNetPokemonBattle.Models;
using System.Media;
using static DotNetPokemonBattle.Models.Pokemon;
using System.Text.Json;
using Newtonsoft.Json;
using DotNetPokemonBattle.Controllers;
using System;

namespace NetcoreDemo.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult Index()
        {
            var gameState = HttpContext.Session.GetObjectFromJson<GameState>("GameState");

            if (gameState == null)
            {
                var player = new Character
                {
                    Name = "Pikachue",
                    HP = 100,
                    AttackPower = 20,
                    Moves = new List<Move>
                    {
                        new Move { Name = "Thunderbolt", Damage = 15},
                        new Move { Name = "Quick Attack", Damage = 10}
                    }
                };

                var enemy = new Character
                {
                    Name = "Charmander",
                    HP = 112,
                    AttackPower = 20,
                    Moves = new List<Move>
                    {
                        new Move { Name = "Ember", Damage = 18},
                        new Move { Name = "Scratch", Damage = 10}
                    }
                };

                // Here I set up the game state of the game we will play
                gameState = new GameState
                {
                    Player = player,
                    Enemy = enemy,
                    IsPlayerTurn = true
                };

            }

            // I am using Json here as  way to preserve data as I struggled with temp data to work 
            HttpContext.Session.SetObjectAsJson("GameState", gameState);

            return View(gameState);
        }
        [HttpPost]
        public IActionResult Attack(string moveName)
        {
            var gameState = HttpContext.Session.GetObjectFromJson<GameState>("GameState");

            if (gameState.IsPlayerTurn)
            {
                var move = gameState.Player.Moves.FirstOrDefault(m => m.Name == moveName);
                if (move != null)
                {
                    gameState.Enemy.HP -= move.Damage;
                    gameState.IsPlayerTurn = false;
                }
            }
            else
            {
                // Enemy's turn logic (could be random move selection)
                var move = gameState.Enemy.Moves.First();
                gameState.Player.HP -= move.Damage;
                gameState.IsPlayerTurn = true;
            }

            HttpContext.Session.SetObjectAsJson("GameState", gameState);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            HttpContext.Session.Remove("GameState");
            return RedirectToAction("Index");
        }
    }
}