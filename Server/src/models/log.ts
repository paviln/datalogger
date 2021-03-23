import mongoose, {Document, Schema} from 'mongoose';

export interface ILog extends Document {
  temperature: number,
  airHumidity: number,
  soilHumidity: number,
  plantId: Schema.Types.ObjectId
}

const LogSchema: Schema = new Schema(
    {
      temperature: {
        type: Number,
        required: true,
      },
      air_humidity: Number,
      soil_humidity: Number,
      plantId: {
        type: Schema.Types.ObjectId,
        ref: 'Plant',
      },
    },
    {
      timestamps: true,
    },
);

export default mongoose.model<ILog>('Log', LogSchema);
