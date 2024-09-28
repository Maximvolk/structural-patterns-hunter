
---
>                        Proxy Design Pattern
>            
>Intent: Lets you provide a substitute or placeholder for another object. A
>proxy controls access to the original object, allowing you to perform
>something either before or after the request gets through to the original
>object.    

* ISubject (../design-patterns-csharp/Proxy.Conceptual/Program.cs, line 26) -> Subject interface  
* RealSubject (../design-patterns-csharp/Proxy.Conceptual/Program.cs, line 41) -> Real subject  
* Proxy (../design-patterns-csharp/Proxy.Conceptual/Program.cs, line 52) -> Proxy  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* ConcreteMediator (../design-patterns-csharp/Mediator.Conceptual/Program.cs, line 33) -> Facade  
* Component1 (../design-patterns-csharp/Mediator.Conceptual/Program.cs, line 90) -> Subsystem  
* Component2 (../design-patterns-csharp/Mediator.Conceptual/Program.cs, line 107) -> Subsystem  

---
>                        Composite Design Pattern
>
>Intent: Lets you compose objects into tree structures and then work with
>these structures as if they were individual objects.    

* Component (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 21) -> Base component  
* Composite (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 97) -> Concrete composite  
* ConcreteComponent (../design-patterns-csharp/Decorator.Conceptual/Program.cs, line 30) -> Concrete leaf  
* Leaf (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 77) -> Concrete leaf  

---
>                        Decorator Design Pattern
>
>Intent: Lets you attach new behaviors to objects by placing these objects
>inside special wrapper objects that contain the behaviors.    

* Component (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 21) -> Base component  
* ConcreteComponent (../design-patterns-csharp/Decorator.Conceptual/Program.cs, line 30) -> Concrete component  
* Composite (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 97) -> Concrete component  
* Leaf (../design-patterns-csharp/Composite.Conceptual/Program.cs, line 77) -> Concrete component  
* Decorator (../design-patterns-csharp/Decorator.Conceptual/Program.cs, line 49) -> Base decorator  
* ConcreteDecoratorA (../design-patterns-csharp/Decorator.Conceptual/Program.cs, line 84) -> Concrete decorator  
* ConcreteDecoratorB (../design-patterns-csharp/Decorator.Conceptual/Program.cs, line 108) -> Concrete decorator  

---
>                        Flyweight Design Pattern
>
>Intent: Lets you fit more objects into the available amount of RAM by sharing
>common parts of state between multiple objects, instead of keeping all of the
>data in each object.    

* Flyweight (../design-patterns-csharp/Flyweight.Conceptual/Program.cs, line 33) -> Flyweight  
* Car (../design-patterns-csharp/Flyweight.Conceptual/Program.cs, line 125) -> Common State object  
* FlyweightFactory (../design-patterns-csharp/Flyweight.Conceptual/Program.cs, line 59) -> Flyweight Factory  

---
>                        Bridge Design Pattern
>
>Intent: Lets you split a large class or a set of closely related classes into
>two separate hierarchies—abstraction and implementation—which can be
>developed independently of each other.
>
>              A
>           /     \                        A         N
>         Aa      Ab        ===>        /     \     / \
>        / \     /  \                 Aa(N) Ab(N)  1   2
>      Aa1 Aa2  Ab1 Ab2    

* Abstraction (../design-patterns-csharp/Bridge.Conceptual/Program.cs, line 36) -> A  
* ExtendedAbstraction (../design-patterns-csharp/Bridge.Conceptual/Program.cs, line 56) -> Ai(N)  
* IImplementation (../design-patterns-csharp/Bridge.Conceptual/Program.cs, line 80) -> N  
* ConcreteImplementationB (../design-patterns-csharp/Bridge.Conceptual/Program.cs, line 98) -> Ni  
* ConcreteImplementationA (../design-patterns-csharp/Bridge.Conceptual/Program.cs, line 90) -> Ni  

---
>                        Facade Design Pattern
>
>Intent: Provides a simplified interface to a library, a framework, or any
>other complex set of classes.    

* Facade (../design-patterns-csharp/Facade.Conceptual/Program.cs, line 26) -> Facade  
* Subsystem1 (../design-patterns-csharp/Facade.Conceptual/Program.cs, line 64) -> Subsystem  
* Subsystem2 (../design-patterns-csharp/Facade.Conceptual/Program.cs, line 80) -> Subsystem  
