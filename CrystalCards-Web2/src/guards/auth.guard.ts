import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    // tslint:disable-next-line:no-trailing-whitespace
   /** private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService **/) {

    }

  canActivate(): boolean  {
    /**if(this.authService.loggedIn()){
      return true;
    }
    this.alertify.error('You need to be logged in');
    this.router.navigate(['']);
  }**/
return true;
  }
}
