import { Component, OnInit } from "@angular/core";
import { StatService } from "./stat.service";
import { Stat } from "./stat.model";

@Component({
    selector: 'app-stat-list',
    templateUrl: './stat-list.component.html'
})
export class StatListComponent implements OnInit{
    stats:Stat[] = [];

    constructor(private StatService: StatService) {}

    ngOnInit(): void {
        this.StatService.getStats().subscribe(
            data=>{
                this.stats=data;
            },
            error =>{
                console.error('Ошибка при получении данных', error);
            }
    );
    }
}