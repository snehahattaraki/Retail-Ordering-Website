import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { Loading } from '../services/loading';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loading = inject(Loading);
  return loading.track(next(req));
};
