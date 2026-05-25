export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

export const API = {
  auth: {
    login: 'auth/login',
    register: 'auth/register',
  },
};

export const STORAGE = {
  token: 'food-ordering.token',
};