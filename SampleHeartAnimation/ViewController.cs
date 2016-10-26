using System;
using CoreAnimation;
using UIKit;
using CoreGraphics;
using Foundation;

namespace SampleHeartAnimation
{
	public partial class ViewController : UIViewController
	{
		private CAEmitterLayer heartsEmitter = new CAEmitterLayer();
		protected ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			heartsEmitter.Position = new CGPoint(BtnSpreadLove.Frame.X + BtnSpreadLove.Frame.Size.Width / 2.0f,
												 BtnSpreadLove.Frame.Y + BtnSpreadLove.Frame.Size.Height / 2.0f);
			heartsEmitter.Size = BtnSpreadLove.Bounds.Size;

			heartsEmitter.Mode = "kCAEmitterLayerVolume";
			heartsEmitter.Shape = "kCAEmitterLayerRectangle";
			heartsEmitter.RenderMode = "kCAEmitterLayerAdditive";

			CAEmitterCell heart = new CAEmitterCell();
			heart.Name = "heart";
			heart.EmissionLongitude = (nfloat)Math.PI / 2.0f;
			heart.EmissionRange = 0.55f * (nfloat)Math.PI;
			heart.BirthRate = 0.0f;
			heart.LifeTime = 10.0f;
			heart.Velocity = -120;
			heart.VelocityRange = 60;
			heart.AccelerationY = 20;
			heart.Contents = UIImage.FromBundle("DazHeart").CGImage;
			heart.Color = UIColor.FromRGBA(0.5f, 0.0f, 0.5f, 0.5f).CGColor;
			heart.RedRange = 0.3f;
			heart.BlueRange = 0.3f;
			heart.AlphaSpeed = -0.5f / heart.LifeTime;
			heart.Scale = 0.15f;
			heart.ScaleSpeed = 0.5f;
			heart.SpinRange = 2.0f * (nfloat)Math.PI;
			heartsEmitter.Cells = new CAEmitterCell[] { heart };
			this.View.Layer.AddSublayer(heartsEmitter);

			BtnSpreadLove.TouchUpInside += (sender, e) => {
				var heartsBurst = CABasicAnimation.FromKeyPath("emitterCells.heart.birthRate");
				heartsBurst.From = NSNumber.FromFloat(10.0f);
				heartsBurst.To = NSNumber.FromFloat(0.0f);
				heartsBurst.Duration = 15.0f;
				heartsBurst.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
				heartsEmitter.AddAnimation(heartsBurst, "heartsBurst");
			};
		}

		public override void ViewWillUnload()
		{
			base.ViewWillUnload();
			heartsEmitter.RemoveFromSuperLayer();
			heartsEmitter = null;
		}
	}
}
