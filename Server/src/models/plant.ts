import mongoose, {Document, Schema} from 'mongoose';

export enum SoilType {
  // eslint-disable-next-line no-unused-vars
  DRY = 1,
  // eslint-disable-next-line no-unused-vars
  WET
}

export enum Status {
  // eslint-disable-next-line no-unused-vars
  ACTIVE = 1,
  // eslint-disable-next-line no-unused-vars
  DONE
}

export interface IPlant extends Document {
  name: String,
  minimumTemperature: Number,
  soilType: SoilType
  img:
  {
    data: Buffer,
    contentType: String
  },
  status: Status,
  logs: Schema.Types.ObjectId[],
  loggerId: mongoose.Types._ObjectId
}

const PlantSchema = new Schema(
    {
      name: {
        type: String,
        required: true,
      },
      minimumTemperature: {
        type: String,
        required: true,
      },
      soilType: {
        type: Number,
        enum: SoilType,
        required: true,
      },
      img:
      {
        data: Buffer,
        contentType: String,
        select: false,
      },
      status: {
        type: Number,
        enum: Status,
        default: Status.ACTIVE,
        required: true,
      },
      logs: [{
        type: Schema.Types.ObjectId,
        ref: 'Logs',
      }],
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
