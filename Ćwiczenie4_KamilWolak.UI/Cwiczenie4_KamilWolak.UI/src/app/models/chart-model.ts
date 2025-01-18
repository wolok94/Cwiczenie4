import { SeriesModel } from "./series-model";

export interface ChartModel{
    name : string;
    series : SeriesModel[];
}