import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class LocalStorageService {

  constructor(){ }

    setUserLogin(login: string) { localStorage.setItem('login', login); }
    getUserLogin():string { return localStorage.getItem('login'); }

    setUserRole(role: string) { localStorage.setItem('role', role); }
    getUserRole():string  { return localStorage.getItem('role'); }

    setJwt(jwt: string) { localStorage.setItem('jwt', jwt); }
    getJwt():string { return localStorage.getItem('jwt'); }

    setAuthorizationWordDate(workDate: Date) { localStorage.setItem('workDate', workDate.toString()); }
    getAuthorizationWordDate():Date { return new Date(Date.parse(localStorage.getItem('workDate'))); }

    getProductCart():number[]{
      
      let contain = localStorage.getItem('productCart');
      if(contain==null || contain.length==0) {
        this.deleteAllProductCart();
      }
        

      let storedCart = JSON.parse(localStorage.getItem('productCart'));
      if(storedCart == null)
        return[];
      return storedCart;
    }

    addToCart(idProduct: number)
    {
      var storedCart: number[] = this.getProductCart();
      storedCart.push(idProduct);
      localStorage.setItem("productCart", JSON.stringify(storedCart));
      
    }

    deleteProductCart(idProduct:number){
      var storedCart: number[] = this.getProductCart();
      const index = storedCart.indexOf(idProduct, 0);
      if (index > -1) {
        storedCart.splice(index, 1);
      }  
      
      localStorage.setItem("productCart", JSON.stringify(storedCart));
    }

    deleteAllProductCart(){
      var storedCart: number[] =[];
      localStorage.setItem("productCart", JSON.stringify(storedCart));
    }
    


    saveAuthorizationUser(login: string, role: string, jwt: string, workDate: Date){
      this.setUserLogin(login);
      this.setUserRole(role);
      this.setJwt(jwt);
      this.setAuthorizationWordDate(workDate); 
    }

    logOutUser(){
      this.setUserLogin('');
      this.setUserRole('');
      this.setJwt('');
      this.setAuthorizationWordDate(new Date(Date.now()));
    }
 
}