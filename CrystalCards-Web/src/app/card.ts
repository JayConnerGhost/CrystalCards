import { NPPoint } from './cards/NPPoint';
import { ActionPoint } from './cards/ActionPoint';
export class Card {
    id:number;
    title: string;
    description: string;
    npPoints: NPPoint[];
    order: number;
  actionPoints: ActionPoint[];
}

