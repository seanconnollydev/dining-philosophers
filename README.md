dining-philosophers
===================

A C# implementation of the Dining Philosopher's problem using the monitor object pattern.

Instructions
============

The Dining Philosophers problem is as follows:  A group of philosophers are sitting down at a circular table with food in the middle of the table, and a chopstick on each side of each philosopher.  At any time, they are either thinking or eating.  In order to eat, they need to have two chopsticks.  If the chopstick to their left or right is currently being used, they must wait for the other philosopher to put it down.  You may notice that if each philosopher decides to eat at the same time, and each picks up the chopstick to his or her right, he or she will not be able to eat, because everyone is waiting for the chopstick on their left.  This situation is called “deadlock”.  In this assignment, you will use the Monitor Object pattern in an algorithm to prevent deadlock.

You are to design a simple model of the Dining Philosophers problem in C# and an algorithm using the CLR-native implementation of the  Monitor Object pattern to prevent deadlock.  There should be 5 philosophers and 5 chopsticks, and each philosopher should eat exactly five times, and be represented by a Thread.  The program should create output that looks something like this:

Dinner is starting!

Philosopher 1 picks up left chopstick.
Philosopher 1 picks up right chopstick.
Philosopher 1 eats.
Philosopher 3 picks up left chopstick
Philosopher 1 puts down right chopstick.
Philosopher 3 picks up right chopstick.
Philosopher 2 picks up left chopstick.
Philosopher 1 puts down left chopstick.
Philosopher 3 eats.
Philosopher 2 picks up right chopstick.
Philosopher 2 eats.
Philosopher 2 puts down right chopstick.
Philosopher 2 puts down left chopstick.
Philosopher 3 puts down right chopstick.
Philosopher 3 puts down left chopstick.
…
Dinner is over!

It is up to you to apply the Monitor Object pattern in C# to prevent deadlock 
