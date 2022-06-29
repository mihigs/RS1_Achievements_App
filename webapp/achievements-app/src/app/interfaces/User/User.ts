import { Achievement } from "../Achievements/Achievement"
import { OrganizedEvent } from "../Events/OrganizedEvent"
import { Team } from "../Teams/Team"

export class User{
    id: number
    firstName : string
    lastName : string
    teams: Team[]
    achievements: Achievement[]
    events: OrganizedEvent[]
}
