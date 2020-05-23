export class Hero {

    constructor(
      public id: number,
      public name: string,
      public power: string,
      public alterEgo?: string
    ) {  }
  
  }

  const heros = [
    new Hero(1,'zhusir','不吃早饭','熬夜'),
    new Hero(2,'亚索','疾风斩','面对疾风把'),
    new Hero(3,'瑞文','断剑','呀!!'),
    new Hero(4,'劫','手里剑','影杀阵'),
    new Hero(5,'猴子','破碎重击','大风车')
  ]
  