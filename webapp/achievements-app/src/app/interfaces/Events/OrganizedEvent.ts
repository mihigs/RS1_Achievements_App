import { Achievement } from "../Achievements/Achievement"

export class OrganizedEvent{
    id: number
    name : string
    description : string
    eventAchievements: Achievement[]
    created: Date
    createdBy: string
    attendees: string[]
}
