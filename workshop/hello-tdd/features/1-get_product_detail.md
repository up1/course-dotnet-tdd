# REST API Specification
* Get product information by id (number only and required field)

## API specification

### Request
GET /v1/product/:id
* id = number only

### Response

case 1 :: Success
* Example request = GET /v1/product/1
* response code = 200
```
{
    "id": 1,
    "name": "Product 01",
    "price": 100.55
}
```

case 2 :: Product not found
* Example request = GET /v1/product/2
* response code = 404
```
{
    "message": "Product id = 2 not found in system"
}

case 3 :: System error (Database error)
* Example request = GET /v1/product/3
* response code = 500
```
{
    "message": "System error"
}

