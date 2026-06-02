export interface OrderItem {
  id: number;
  productId: number;
  productName: string;
  quantity: number;
  price: number;
}

export interface Order {
  id: number;
  userId: number;
  totalAmount: number; // Changed from 'total'
  status: string;
  createdAt: string;
  orderItems: OrderItem[]; // Changed from 'items'
}