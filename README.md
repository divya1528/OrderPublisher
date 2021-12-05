# OrderPublisher

## This API publishes the new order requests to kafka with order status as placed. Along with message queue, the order details are saved in mongodb as well.

**To place new order, use below request**

```
POST
http://localhost:5000

Body:
{
  "id": "",
  "userID": "test",
  "products": [

	{
      "productID": "2",
      "productQty": 10,
      "productPrice": 50
    }
  ],
  "status": "placed"
}

```

**The Order ID is created in database and returned through API response.**

API Response:
```
{
  "id": "qer1234fsfsf",
  "userID": "test",
  "products": [

	{
      "productID": "2",
      "productQty": 10,
      "productPrice": 50
    }
  ],
  "status": "placed"
}
```

USECASE Diagram:
https://github.com/divya1528/OrderPublisher/blob/main/Order%20Processing.png
