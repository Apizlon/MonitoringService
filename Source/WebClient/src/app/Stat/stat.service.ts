import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http"
import { Observable } from "rxjs";
import { Stat } from "./stat.model";
import { Event } from "../Events/event.model";

@Injectable({
    providedIn: 'root'
})
export class StatService {
    private apiUrl = 'https://localhost:7067/api/View'
    constructor(private http: HttpClient) { }

    getStats(): Observable<Stat[]>{
        return this.http.get<Stat[]>(this.apiUrl);
    }

    getEventsByStatId(statId: number): Observable<Event[]>{
        return this.http.get<Event[]>(`${this.apiUrl}/${statId}`);
    }
}