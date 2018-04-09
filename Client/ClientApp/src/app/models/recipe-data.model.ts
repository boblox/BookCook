export class RecipeData {
    name: string;
    description: string;

    public constructor(init?: Partial<RecipeData>) {
        Object.assign(this, init);
    }
}
