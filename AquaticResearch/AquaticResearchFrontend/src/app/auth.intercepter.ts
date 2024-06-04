import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from './environments/environment';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  console.log('intercepted request', request);
  const jwt = sessionStorage.getItem('jwt');
  const isLoggedIn = jwt;
  const isApiUrl = request.url.startsWith(environment.apiBaseUrl);
  console.log('intercepted request Logged in', isLoggedIn, jwt, isApiUrl);

  console.log('-----');
  console.log(jwt);
  console.log(isApiUrl);
  if (isLoggedIn && isApiUrl) {
    console.log('intercepted request Logged in');
    request = request.clone({
      setHeaders: { Authorization: `Bearer ${jwt}` }
    });
    console.log('intercepted request', request)
  }

  return next(request);
}