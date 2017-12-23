import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from './../../../environments/environment';
import { IProduct } from './../interface';
@Injectable()
export class ProductService {

    constructor(private http: HttpClient) {
    }

    public save(product: IProduct): Observable<any> {        
        const url = environment.serviceUrl + '/api/product';
        return this.http.post<any>(url, product);
    }

    public update(product: IProduct): Observable<any> {
        const url = environment.serviceUrl + '/api/product';
        return this.http.put<any>(url, product);
    }

    public remove(code: string, isXml: boolean): Observable<any> {
        const url = environment.serviceUrl + '/api/product/' + code + '/' + isXml;
        return this.http.delete<any>(url);
    }

    public search(filterValue: string, isXml: boolean): Observable<any> {
        const url = environment.serviceUrl + '/api/product/search/' + isXml + '?filterValue=' + filterValue;
        return this.http.get<any>(url);
    }
}