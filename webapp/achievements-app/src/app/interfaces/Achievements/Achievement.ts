import { OrganizedEvent } from "../Events/OrganizedEvent"
import { Team } from "../Teams/Team"

export class Achievement{
    id: number
    name : string
    description : string
    eventId: number
    event: OrganizedEvent
    teamId: number
    team: Team
    created: Date
    createdBy: number
    tier: string
    achievedBy: []
}
