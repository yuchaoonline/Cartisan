//using System.Web.Mvc;
//
//namespace Cartisan.Web.Mvc {
//    public class FluentValidationConfig {
//        public static void Initialize() {
//            FluentValidationModelValidatorProvider fluentValidationModelValidatorProvider =
//                new FluentValidationModelValidatorProvider(new ModelValidatorFactory());
//
//            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
//            fluentValidationModelValidatorProvider.AddImplicitRequiredValidator = false;
//
//            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);
//        } 
//    }
//
//    public class ModelValidatorFactory: ValidatorFactoryBase {
//        public override IValidator CreateInstance(Type validatorType) {
//            IValidator validator = DependencyResolver.Current.GetService(validatorType) as IValidator;
//            return validator;
//        }
//    }
//}