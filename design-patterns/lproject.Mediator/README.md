### **What is a Mediator?**

**Mediator is a design pattern that coordinates interactions among components.**

Instead of letting components **reference and call each other directly**, they all communicate **through a single mediator**.

When a component needs another component to perform some action, it simply **sends a message to the mediator**, and the mediator is responsible for **forwarding the request to the appropriate component**.

---

### **Why Mediator?**

When objects communicate directly, their logic becomes tightly coupled to one another.

For example, an input field for “username” may contain validation logic specifically for usernames. If another input field needs to reuse the same validation logic but differs only in visual representation, code duplication becomes unavoidable because the components are directly dependent on each other.

By removing direct dependencies, we can easily extend, maintain, and reuse components.

A mediator manages all relationships between components, allowing new interactions to be added without modifying existing components.

---

### **When to Use Mediator?**

You can use the mediator pattern when you find yourself creating many subclasses solely to reuse a few shared behaviors in different contexts.

For example, a username input on a login form requires a placeholder “username” and triggers username-specific validation. That same input component cannot be reused for “product name” input because the validation and behavior are tied to the username context.

By introducing a mediator, both the input component and the validation logic communicate indirectly through the mediator and receive the context parameter (e.g., username vs product name). This allows components to stay generic while the mediator handles contextual coordination.

---

### **How to Implement Mediator**

![image.png](../../asserts/structure-mediator.png)

**Components** are elements in the application that need to communicate.

They all depend on a **Mediator interface** that exposes a `notify` method — this is the mechanism components use to signal events to the mediator.

The mediator then invokes the appropriate components according to the defined relationships.

Each component knows nothing about the others; they only interact through the mediator, following the **Single Responsibility Principle**.

**Mediator** is an interface that allows multiple mediator implementations, satisfying the **Open/Closed Principle**.

**ConcreteMediator** implements a specific coordination logic for a given context.

Different contexts can have different mediators, and components can attach to the mediator in various ways depending on the relationships needed.

A limitation of the Mediator pattern is that the mediator object may gradually grow into a **God Object** if it centralizes too much logic.

---

- Identify the group of components that need to interact with each other.
- Create a **Mediator interface** with methods that components will use to communicate with the mediator.
- Implement a concrete mediator for the chosen context, allowing it to coordinate the components in the identified group.
- Update components so they depend on the mediator interface and call mediator methods whenever they need to interact.