// TODO: Compiler version and framework
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DiningPhilosophers
{
    class Program
    {
        private const int PHILOSOPHER_COUNT = 5, TIMES_TO_EAT = 5;

        static void Main(string[] args)
        {
            var philosophers = InitializePhilosophers();

            Console.WriteLine("Dinner is starting!");

            var philosopherThreads = new List<Thread>();
            foreach (var philosopher in philosophers)
            {
                var philosopherThread = new Thread(new ThreadStart(philosopher.EatAll));
                philosopherThreads.Add(philosopherThread);

                philosopherThread.Start();
            }

            foreach (var thread in philosopherThreads)
            {
                thread.Join();
            }

            Console.WriteLine("Dinner is over!");
        }

        private static List<Philosopher> InitializePhilosophers()
        {
            var philosophers = new List<Philosopher>(PHILOSOPHER_COUNT);
            for (int i = 0; i < PHILOSOPHER_COUNT; i++)
            {
                philosophers.Add(new Philosopher(philosophers, i));
            }

            return philosophers;
        }
    }

    [DebuggerDisplay("Name = {Name}")]
    public class Philosopher
    {
        private const int TIMES_TO_EAT = 5;
        private int _timesEaten = 0;
        private readonly List<Philosopher> _allPhilosophers;
        private readonly int _index;
        public Philosopher(List<Philosopher> allPhilosophers, int index)
        {
            _allPhilosophers = allPhilosophers;
            _index = index;
            this.Name = string.Format("Philosopher {0}", _index);
            this.State = State.Thinking;
        }

        public string Name { get; private set; }

        public State State { get; private set; }

        public Philosopher LeftPhilosopher
        {
            get
            {
                if (_index == 0)
                    return _allPhilosophers[_allPhilosophers.Count - 1];
                else
                    return _allPhilosophers[_index - 1];
            }
        }

        public Philosopher RightPhilosopher
        {
            get
            {
                if (_index == _allPhilosophers.Count - 1)
                    return _allPhilosophers[0];
                else
                    return _allPhilosophers[_index + 1];
            }
        }

        public void EatAll()
        {
            // Cycle through thinking and eating until done eating.
            while (_timesEaten < TIMES_TO_EAT)
            {
                this.Think();
                this.PickUp();
                this.Eat();
                this.PutDown();
            }
        }

        private void PickUp()
        {
            // If either left or right is eating, wait for chopsticks.
            if (this.LeftPhilosopher.State == State.Eating || this.RightPhilosopher.State == State.Eating)
            {
                Monitor.Wait(this);
                this.State = State.Hungry;
            }

            Console.WriteLine(this.Name + " picks up left chopstick.");
            Console.WriteLine(this.Name + " picks up right chopstick.");
        }

        private void PutDown()
        {
            // Give left neighbor a chance to eat.
            if (this.LeftPhilosopher.State == State.Hungry &&
                this.LeftPhilosopher.LeftPhilosopher.State != State.Eating)
            {
                Monitor.Pulse(this.LeftPhilosopher);
            }

            // Give right neighbor a chance to eat.
            if (this.RightPhilosopher.State == State.Hungry &&
                this.RightPhilosopher.RightPhilosopher.State != State.Eating)
            {
                Monitor.Pulse(this.RightPhilosopher);
            }

            Console.WriteLine(this.Name + " puts down left chopstick.");
            Console.WriteLine(this.Name + " puts down right chopstick.");
        }

        private void Eat()
        {
            this.State = State.Eating;
            _timesEaten++;
            Console.WriteLine(this.Name + " eats.");
        }

        private void Think()
        {
            this.State = State.Thinking;
        }
    }

    public enum State
    {
        Thinking = 0,
        Hungry = 1,
        Eating = 2
    }
}