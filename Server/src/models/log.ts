import mongoose, {Document, Schema} from 'mongoose';

export interface ILog extends Document {
    temperature: number,
    airHumidity: number,
    soilHumidity: number,
    timestamp: Date,
    loggerId: Schema.Types.ObjectId
}

const LogSchema: Schema = new Schema(
    {
      temperature: {
        type: Number,
        required: true,
      },
      air_humidity: Number,
      soil_humidity: Number,
      loggerId: {
        type: Schema.Types.ObjectId,
        ref: 'Logger',
      },
    },
    {
      timestamps: true,
    },
);

export default mongoose.model<ILog>('Log', LogSchema);
