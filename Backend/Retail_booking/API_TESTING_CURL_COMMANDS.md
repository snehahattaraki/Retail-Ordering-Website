# API Testing Commands (cURL)

## Base URL
```
http://localhost:5000
```

## 1. AUTHENTICATION APIs

### Register User
```bash
curl -X POST http://localhost:5000/api/Auth/register \
  -H "Content-Type: application/json" \
  -d '{
	"name": "Test User",
	"email": "test@example.com",
	"password": "Password123!"
  }'
```

### Login (Admin)
```bash
curl -X POST http://localhost:5000/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
	"email": "admin@retail.com",
	"password": "Admin123!"
  }'
```

**Save the returned token for other requests**

### Get Profile
```bash
curl -X GET http://localhost:5000/api/Auth/profile \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

---

## 2. ADMIN APIs

### Get Dashboard Stats
```bash
curl -X GET http://localhost:5000/api/Admin/stats \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Admin stats retrieved",
  "data": {
	"totalProducts": 2,
	"totalOrders": 0,
	"totalUsers": 1,
	"totalRevenue": 0.00
  }
}
```

### Get All Users
```bash
curl -X GET http://localhost:5000/api/Admin/users \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Users retrieved",
  "data": [
	{
	  "id": 1,
	  "name": "Admin",
	  "email": "admin@retail.com",
	  "role": "Admin"
	}
  ]
}
```

### Create New User
```bash
curl -X POST http://localhost:5000/api/Admin/users \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
	"name": "New User",
	"email": "newuser@example.com",
	"role": "Customer"
  }'
```

### Update User
```bash
curl -X PUT http://localhost:5000/api/Admin/users/2 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
	"name": "Updated Name",
	"email": "updated@example.com",
	"role": "Customer"
  }'
```

### Delete User
```bash
curl -X DELETE http://localhost:5000/api/Admin/users/2 \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Get Low Stock Products
```bash
curl -X GET http://localhost:5000/api/Admin/products/low-stock \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Get Orders by Status
```bash
curl -X GET http://localhost:5000/api/Admin/orders/status \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Update Order Status
```bash
curl -X PUT http://localhost:5000/api/Admin/orders/1/status \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
	"status": "Shipped"
  }'
```

---

## 3. PRODUCT APIs

### Get Products with Pagination
```bash
curl -X GET "http://localhost:5000/api/Products?page=1&pageSize=10"
```

**With Filters:**
```bash
curl -X GET "http://localhost:5000/api/Products?page=1&pageSize=10&search=pizza&category=Pizza&brand=House"
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Products retrieved",
  "data": {
	"totalItems": 1,
	"page": 1,
	"pageSize": 10,
	"totalPages": 1,
	"items": [...]
  }
}
```

### Get Product by ID
```bash
curl -X GET http://localhost:5000/api/Products/1
```

### Get All Brands
```bash
curl -X GET http://localhost:5000/api/Products/brands
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Brands retrieved",
  "data": ["House"]
}
```

### Get All Categories
```bash
curl -X GET http://localhost:5000/api/Products/categories
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Categories retrieved",
  "data": ["Pizza", "Cold Drinks", "Bread"]
}
```

---

## 4. CATEGORY APIs

### Get All Categories
```bash
curl -X GET http://localhost:5000/api/Categories
```

### Get Category by ID
```bash
curl -X GET http://localhost:5000/api/Categories/1
```

### Create Category (Admin)
```bash
curl -X POST http://localhost:5000/api/Categories \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN" \
  -d '{
	"name": "Beverages"
  }'
```

### Update Category (Admin)
```bash
curl -X PUT http://localhost:5000/api/Categories/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN" \
  -d '{
	"name": "Updated Category"
  }'
```

### Delete Category (Admin)
```bash
curl -X DELETE http://localhost:5000/api/Categories/1 \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN"
```

---

## 5. ORDER APIs

### Get User's Orders
```bash
curl -X GET http://localhost:5000/api/Orders \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Get Specific Order
```bash
curl -X GET http://localhost:5000/api/Orders/1 \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Get Order Items
```bash
curl -X GET http://localhost:5000/api/Orders/1/items \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Place New Order
```bash
curl -X POST http://localhost:5000/api/Orders/place \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
	"shippingAddress": "123 Main Street, City, State",
	"phone": "555-1234",
	"paymentMethod": "COD",
	"items": [
	  {
		"productId": 1,
		"quantity": 2
	  },
	  {
		"productId": 2,
		"quantity": 1
	  }
	]
  }'
```

**Expected Response:**
```json
{
  "success": true,
  "message": "Order placed successfully",
  "data": {
	"orderId": 1,
	"totalAmount": 20.97
  }
}
```

### Reorder (Copy Previous Order)
```bash
curl -X POST http://localhost:5000/api/Orders/1/reorder \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### Update Order Status (Admin)
```bash
curl -X PUT http://localhost:5000/api/Orders/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN" \
  -d '{
	"status": "Shipped"
  }'
```

---

## 6. INVENTORY APIs

### Get All Inventory (Admin)
```bash
curl -X GET http://localhost:5000/api/Inventory \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN"
```

### Add to Inventory (Admin)
```bash
curl -X POST http://localhost:5000/api/Inventory \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN" \
  -d '{
	"name": "New Product",
	"category": "Pizza",
	"price": 12.99,
	"stockQty": 50
  }'
```

### Update Inventory (Admin)
```bash
curl -X PUT http://localhost:5000/api/Inventory/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN" \
  -d '{
	"name": "Updated Product",
	"category": "Pizza",
	"price": 13.99,
	"stockQty": 45
  }'
```

### Delete from Inventory (Admin)
```bash
curl -X DELETE http://localhost:5000/api/Inventory/1 \
  -H "Authorization: Bearer YOUR_ADMIN_TOKEN"
```

---

## 7. PAYMENT APIs

### Process Payment
```bash
curl -X POST http://localhost:5000/api/Payments/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
	"amount": 20.97,
	"method": "COD"
  }'
```

---

## PowerShell Commands (for Windows)

### Login and Save Token
```powershell
$response = Invoke-WebRequest -Uri "http://localhost:5000/api/Auth/login" `
  -Method POST `
  -ContentType "application/json" `
  -Body '{"email":"admin@retail.com","password":"Admin123!"}'

$token = ($response.Content | ConvertFrom-Json).data.token
Write-Host "Token: $token"
```

### Use Token in Request
```powershell
$headers = @{
  "Authorization" = "Bearer $token"
}

$admin_stats = Invoke-WebRequest -Uri "http://localhost:5000/api/Admin/stats" `
  -Headers $headers

$admin_stats.Content | ConvertFrom-Json | ConvertTo-Json -Depth 10
```

---

## Error Response Examples

### 401 Unauthorized (Missing Token)
```json
{
  "success": false,
  "message": "Unauthorized",
  "data": null
}
```

### 403 Forbidden (Not Admin Role)
```json
{
  "success": false,
  "message": "Forbidden",
  "data": null
}
```

### 404 Not Found
```json
{
  "success": false,
  "message": "Product not found",
  "data": null
}
```

### 400 Bad Request
```json
{
  "success": false,
  "message": "Invalid product data",
  "data": null
}
```

---

## Swagger UI

Test all endpoints visually at:
```
http://localhost:5000/swagger/index.html
```

1. Click "Authorize" button
2. Paste token: `Bearer YOUR_TOKEN_HERE`
3. Click "Authorize"
4. Try endpoints directly from Swagger UI

---

## Common Issues and Solutions

### "Invalid token" Error
- Solution: Re-login to get fresh token
- Command: POST /api/Auth/login with credentials

### "401 Unauthorized" on Admin endpoint
- Solution: Use admin user credentials (admin@retail.com / Admin123!)
- Verify token contains "role": "Admin"

### "404 Not Found" on /api/Admin/stats
- Solution: Rebuild solution with `dotnet build`
- Verify AdminController exists at API/Controllers/AdminController.cs

### "Cannot POST /api/Orders/place"
- Solution: Check token is valid and user is authenticated
- Include Authorization header with Bearer token

### Product not found error
- Solution: Create products first via admin
- Default products: ID 1 (Margherita Pizza), ID 2 (Coke)

---

## Token Expiration

Tokens expire after 4 hours. When expired:
```json
{
  "success": false,
  "message": "The token is expired",
  "data": null
}
```

Solution: Re-login to get new token

---

## Performance Tips

- Use pagination: `?page=1&pageSize=10`
- Filter by category: `?category=Pizza`
- Use search: `?search=pizza`
- Combine filters: `?page=1&pageSize=20&category=Pizza&brand=House`

---

## Testing Workflow

1. **Register/Login** → Get JWT token
2. **Get Products** → View available items
3. **Place Order** → Create new order
4. **Get Orders** → View user's orders
5. **Admin Stats** (admin only) → View system stats
6. **Update Order** (admin only) → Change order status
