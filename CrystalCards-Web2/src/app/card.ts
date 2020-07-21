export class Card{
  id: number;
  title: string;
  description: string;
  points: any;
  actionPoints: any;
  links: any;

  constructor(title: string , description: string)
  {
    this.title = title;
    this.description = description;
  }

}
