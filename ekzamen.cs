﻿using System;
using System.Collections.Generic;
namespace _6lab
{
   //task 1; 

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Banana")
            {
                return $"Monkey: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Nut")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "MeatBall")
            {
                return $"Dog: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
    class PeopleHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Cup of coffee")
            {
                return $"People: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
    class Client
    {
        public static void ClientCode(AbstractHandler handler)
        {
            foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee", "MeatBall" })
            {
                Console.WriteLine($"Client: Who wants a {food}?");
                var result = handler.Handle(food);
                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {food} was left untouched.");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var people = new PeopleHandler();
            var dog = new DogHandler();
            monkey.SetNext(squirrel).SetNext(people).SetNext(dog);
            Console.WriteLine("Chain: Monkey > Squirrel > People > Dog\n");
            Client.ClientCode(monkey);
            Console.WriteLine();
            Console.WriteLine("Subchain: Squirrel > Dog\n");
            Client.ClientCode(squirrel);
            Console.WriteLine("Subchain: People > Dog\n");
            Client.ClientCode(people);
        }
    }
}