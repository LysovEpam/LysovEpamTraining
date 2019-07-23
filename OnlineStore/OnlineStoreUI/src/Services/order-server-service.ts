import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { OrderRequest } from 'src/model/entities/apiRequests/orderRequest';
import { OrderSearchRequest } from 'src/model/entities/apiRequests/orderSearchRequest';


@Injectable({
  providedIn: 'root'
})
export class OrderServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    OrderSearchRequest

    getById(id:number, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlOrderGetById(), id, httpOptions);
    }

    getAll(jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlOrderGetAll(), '', httpOptions);
    }

    GetByUser(jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlOrderGetByUser(), null, httpOptions);
    }

    GetBySearch(searchRequest:OrderSearchRequest, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlOrderGetBySearch(), searchRequest, httpOptions);
    }

    create(order: OrderRequest, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlOrderCreate(), order, httpOptions);

    }

    update(order: OrderRequest, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlOrderUpdateOrder(), order, httpOptions);

    }

    delete(id: number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlProductCategoryDelete(), id, httpOptions);

  }

    

    
}
