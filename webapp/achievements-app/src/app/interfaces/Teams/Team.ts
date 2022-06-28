import { Achievement } from "../Achievements/Achievement"

export class Team{
    id: number
    name : string
    description : string
    availableAchievements: Achievement[]
    created: Date
    createdBy: string
    teamMembers: string[]
}
