import mongoose, {Document, Schema} from 'mongoose';

export interface ILogger extends Document {
  plants: Schema.Types.ObjectId[],
}

const LoggerSchema = new Schema(
    {
    },
    {
      timestamps: true,
    },
);

export default mongoose.model<ILogger>('Logger', LoggerSchema);
