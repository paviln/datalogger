import mongoose, {Document, Schema} from 'mongoose';

/**
 * Plant model.
 */
export class Plant {
    test: string;
    loggerId: mongoose.Types._ObjectId;
}

export interface IPlant extends Document {
    test: String,
    loggerId: mongoose.Types._ObjectId
}

const PlantSchema = new Schema(
    {
      test: String,
      loggerId: mongoose.Types._ObjectId,
    },
    {
      timestamps: true,
    },
);

export const Plants = mongoose.model<IPlant>('Plants', PlantSchema);
