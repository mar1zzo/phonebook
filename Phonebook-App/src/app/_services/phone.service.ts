import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonPhone } from '../_models/PersonPhone';

@Injectable({
  providedIn: 'root'
})
export class PhoneService {
  
  baseURL = 'https://localhost:5001/v1/personphones';

  constructor(private http: HttpClient) { }

  getAllPhones(): Observable<PersonPhone[]> {
    return this.http.get<PersonPhone[]>(this.baseURL);
  }

  getPhonesById(id: number): Observable<PersonPhone> {
    return this.http.get<PersonPhone>(`${this.baseURL}/${id}`);
  }

  getPhonesByIdType(id: number): Observable<PersonPhone> {
    return this.http.get<PersonPhone>(`${this.baseURL}/phonetypes/${id}`);
  }

  postPhone(phone: PersonPhone) {
    return this.http.post(this.baseURL, phone);
  }

  putPhone(phone: PersonPhone) {
    return this.http.put(`${this.baseURL}/${phone.id}`, phone);
  }

  deletePhone(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
