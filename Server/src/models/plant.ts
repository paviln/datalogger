import mongoose, {Document, Schema} from 'mongoose';

export interface IPlant extends Document {
  name: String,
  img:
  {
    data: Buffer,
    contentType: String
  },
  loggerId: mongoose.Types._ObjectId
}

const PlantSchema = new Schema(
    {
      name: String,
      img:
      {
        data: Buffer,
        contentType: String
      },
      loggerId: {
        type: Schema.Types.ObjectId,
        ref: 'Logger',
      },
    },
    {
      timestamps: true,
    },
);

export default mongoose.model<IPlant>('Plant', PlantSchema);
