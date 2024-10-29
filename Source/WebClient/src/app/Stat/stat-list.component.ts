import { Component, OnInit } from "@angular/core";
import { StatService } from "./stat.service";
import { Stat } from "./stat.model";
import { Event } from "../Events/event.model";

@Component({
    selector: 'app-stat-list',
    templateUrl: './stat-list.component.html',
    styleUrls: ['./stat-list.component.css']
})
export class StatListComponent implements OnInit{
    stats: Stat[] = [];
    selectedStat: Stat | null = null;
    statEvents: Event[] = [];

    constructor(private StatService: StatService) {}

    ngOnInit(): void {
        this.loadStats();
    }

    loadStats() {
        this.StatService.getStats().subscribe(stats => {
            this.stats = stats; 
        })
    }

    onSelectStat(statId: number){
        this.selectedStat = this.stats.find(stat => stat.id === statId) || null;

        if (this.selectedStat){    
            this.StatService.getEventsByStatId(statId).subscribe(events =>{
                this.statEvents = events;
            })
        }
    }
}