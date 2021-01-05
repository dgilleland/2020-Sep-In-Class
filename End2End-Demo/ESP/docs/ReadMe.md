# ESP - Spec 1

![](./ESP-CustomerDetailsView.png)


The following screen snapshot depicts the form that 
![](./ESP-CustomerOrdersView.png)

---

## ERD

![](./ESP-Physical-ERD-Spec-1.png)

---

### CRUD Page - Manage Customers

### Form to Create Orders

![](./ESP-CustomerOrdersView-UI.png)

---

![](./ESP-CustomerOrdersView-UI-Annotated.png)

```csharp
// Code-first EF w. 1 entity:
from cust in Customers
where cust.Name.Contains("Fre")
select cust
//
context.Products.Find(productId)
//
var ord = context.Orders.Add(new Order());
foreach(var item in SoldProducts)
{
    // Add an item to the order
    // reduce the inventory quantity
}
```
