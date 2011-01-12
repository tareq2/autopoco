using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using System.Collections;
using AutoPoco.Engine;

namespace AutoPoco
{
    public static class StandardExtensions
    {
        /// <summary>
        /// Sets the value of a member directly
        /// </summary>
        public static IEngineConfigurationTypeBuilder<TPoco> Value<TPoco, TMember>(this IEngineConfigurationTypeMemberBuilder<TPoco, TMember> memberConfig,TMember value)
        {
            return memberConfig.Use<ValueSource<TMember>>(value);
        }

        public static IEngineConfigurationTypeBuilder<TPoco> FromParent<TPoco, TMember>(this IEngineConfigurationTypeMemberBuilder<TPoco, TMember> memberConfig)
        {
            return memberConfig.Use<ParentSource<TMember>>();
        }

        public static IEngineConfigurationTypeBuilder<TPoco> Collection<TPoco, TCollection>(
            this IEngineConfigurationTypeMemberBuilder<TPoco, TCollection> memberConfig, int min, int max) where TCollection : IEnumerable
        {
            var collectionType = typeof (TCollection);

            // Nefarious, I know - shocking, this will get is Y from X<Y> where X = IEnumerable/List/Etc and Y = Type
            // So List<Y> etc
            var genericcollectionArgument = collectionType.GetGenericArguments()[0];

            // So this will give us AutoSource<Y>
            var autoSourceType = typeof (AutoSource<>).MakeGenericType(genericcollectionArgument);
            
            //// And this will give us FlexibleEnumerableSource<AutoSource<Y>, X<Y>, Y>
            var enumerableSourceType = typeof (FlexibleEnumerableSource<,,>).MakeGenericType(
                autoSourceType, collectionType, genericcollectionArgument
                );

            // I can't believe I'm doing this, if anybody can give us a good method definition that just_works and 
            // negates the need for this tomfoolery I'm all ears
            var method = memberConfig.GetType().GetMethod("Use",
                                                          System.Reflection.BindingFlags.Public |
                                                          System.Reflection.BindingFlags.Instance, null, new[] { typeof(Object[]) }, null);


           var genericMethod = method.MakeGenericMethod(new[] {enumerableSourceType});
           return (IEngineConfigurationTypeBuilder<TPoco>)genericMethod.Invoke(memberConfig, new object[] { new object[] { min, max}});
        }
    }
}
